using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using SpaPlum.Web.Helpers;
using SpaPlum.Web.Paypal;
using SpaPlum.Web.Paypal.paypalProxy;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaPlum.Web.Controllers
{
    public class PagamentoController : SpaPlumBaseController
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        private GatewayPagamento gateway = GatewayPagamento.Nenhum;

        private bool GatewaySwitch = false;

        // GET: Pagamento
		[Authorize]
        public ActionResult Index()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
	  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
	  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			return View();
        }

        [Authorize]
        public ActionResult ProcessarPayPal(string id)
        {
            if (GatewaySwitch)
            {
                ViewBag.PayPalLink = "";

                gateway = GatewayPagamento.PayPalPadrao;

                Int64 idInteiro = 0;
                if (id != null)
                {
                    Int64.TryParse(id, out idInteiro);
                }

                if (idInteiro > 0)
                {
                    var agendamento = db.AgendamentoModels.Find(id);

                    if (agendamento != null)
                    {

                        PaypalConnector wsPaypal = null;
                        Paypal.paypalProxy.SetExpressCheckoutResponseType retornoPaypal = null;
                        try
                        {
                            // Regra de negócio :: Atribuir o total da venda a uma variável de sessão
                            HttpContext.Session.Add("AMT", agendamento.Valor.ToString().Replace(",", "."));

                            wsPaypal = new SpaPlum.Web.Paypal.PaypalConnector();
                            retornoPaypal = wsPaypal.ExpressCheckout(agendamento,
                                                                     agendamento.EmailDoCliente.ToString(),
                                                                     Convert.ToDecimal(agendamento.Valor),
                                                                     0,
                                                                     agendamento.NomeCliente,
                                                                     agendamento.EmailDoCliente, // Logradouro e Numero
                                                                     agendamento.EmailDoCliente, // Bairro
                                                                     agendamento.EmailDoCliente, // Cidade
                                                                     agendamento.EmailDoCliente, // UF
                                                                     agendamento.EmailDoCliente, // CEP
                                                                     "BR",
                                                                     agendamento.NomeCliente,
                                                                     agendamento.EmailDoCliente, // Logradouro e Numero
                                                                     agendamento.EmailDoCliente, // Bairro
                                                                     agendamento.EmailDoCliente, // Cidade
                                                                     agendamento.EmailDoCliente, // RJ
                                                                     agendamento.EmailDoCliente, // CEP
                                                                     "BR");

                            if ((retornoPaypal.Ack == AckCodeType.Success) || (retornoPaypal.Ack == AckCodeType.SuccessWithWarning))
                            {
                                Session["token"] = retornoPaypal.Token;

                                // - Armazena o token de sucesso retornado do paypal no agendamento
                                // - Altera o Status para Pago (Código 7)
                                agendamento.TokenPayPal = retornoPaypal.Token;
                                agendamento.CodigoStatusAgendamento = 7;
                                db.Entry(agendamento).State = EntityState.Modified;
                                db.SaveChangesAsync();

                                string redirecionamentoPaypal = "";

                                bool isPaypalUseSandbox = true;
                                if (isPaypalUseSandbox)
                                {
                                    redirecionamentoPaypal = string.Format("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={0}&PaymentId={1}", retornoPaypal.Token, agendamento.CodigoAgendamento);
                                }
                                else
                                {
                                    redirecionamentoPaypal = string.Format("https://www.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token={0}&PaymentId={1}", retornoPaypal.Token, agendamento.CodigoAgendamento);
                                }
                                ViewBag.PayPalLink = redirecionamentoPaypal;
                                Response.Redirect(redirecionamentoPaypal, false);
                            }
                            else
                            {
                                PageUtil.MostrarAlertaErro("Transação sem sucesso. Tente novamente mais tarde.");
                                RedirectToAction("PagamentoIndisponivel", "Erro");
                            }
                        }
                        catch (Exception ex)
                        {
                            PageUtil.MostrarAlertaErro(ex.Message.ToString());
                            RedirectToAction("PagamentoIndisponivel", "Erro");
                        }
                        finally
                        {
                            wsPaypal = null;
                            retornoPaypal = null;
                            GC.Collect();
                        }
                    }
                }
            }
            return View();
        }
    }
}