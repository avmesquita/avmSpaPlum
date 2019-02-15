using SpaPlum.Web.Entity;
using SpaPlum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SpaPlum.Web.Controllers
{
    public class MarketingTagsController : Controller
    {
        #region Google Tags        
        public ActionResult GoogleInit()
        {
            return PartialView("_GoogleInit", "");
        }
        /// <summary>
        /// Retorna a ActionResult com Código da Tag do Google Analytics, contendo userID ou não.
        /// </summary>
        /// <returns>Google Analytics Tag Code</returns>
        public ActionResult GoogleAnalytics()
        {
            return PartialView("_GoogleAnalytics", "");
        }
        public ActionResult GoogleRemarketing(Agendamento agendamento)
        {
            GoogleRemarketingTagModel model = new GoogleRemarketingTagModel();

            if (agendamento != null)
            {
                model.idVenda = agendamento.CodigoAgendamento.ToString();
                model.Valor = agendamento.Valor.ToString().Replace(",", ".");
            }
            else
            {
                model.idVenda = "0";
                model.Valor = "0";
            }

            return PartialView("_GoogleRemarketing", model);
        }
        public ActionResult GoogleTagManager(Agendamento agendamento)
        {
            // ##TODO### Levar o código para a view
            var tagGoogle = new StringBuilder();

            tagGoogle.AppendLine(" <script type='text/javascript'> ga('require', 'ecommerce', 'ecommerce.js');");

            // TAG para adicionar compra complena GOOGLE
            tagGoogle.AppendLine(" ga('ecommerce:addTransaction', { ");
            tagGoogle.AppendLine(" 'id': '" + agendamento.CodigoAgendamento + "',   "); // Transaction ID. Required.
            tagGoogle.AppendLine(" 'affiliation': 'Cantão', "); // Affiliation or store name.
            tagGoogle.AppendLine(" 'revenue': '" + agendamento.Valor.ToString().Replace(",", ".") + "',"); // Grand Total.
            tagGoogle.AppendLine(" 'shipping': '" + "0" + "', "); // Shipping.
            tagGoogle.AppendLine("  'tax': '0'   "); // Tax.
            tagGoogle.AppendLine(" }); ");
            
            foreach (var item in agendamento.ItensDoAgendamento)
            {                                
                tagGoogle.AppendLine("  ga('ecommerce:addItem', { "); // Transaction ID. Required.
                tagGoogle.AppendLine(" 'id': '" + item.CodigoAgendamento.ToString() + "', ");
                tagGoogle.AppendLine(" 'name': '" + item.Servico.Nome + "',  ");// Product name. Required.
                tagGoogle.AppendLine(" 'sku': '" + item.Servico.CodigoServico.ToString() + "',   "); // SKU/code.
                tagGoogle.AppendLine(" 'category': '" + item.TipoDeMassagem + "',  "); // Category or variation.
                tagGoogle.AppendLine(" 'price': '" + item.Valor.ToString() + "', "); // Unit price.
                tagGoogle.AppendLine(" 'quantity': '" + item.QtdePeriodos.ToString() + "'  "); // Quantity.
                tagGoogle.AppendLine(" }); ");
            }

            tagGoogle.AppendLine("ga('ecommerce:send'); </script>");

            ViewData["Message"] = tagGoogle.ToString();
            return PartialView();
        }
        #endregion

        #region ShopBack Tags        
        public ActionResult ShopBackInit()
        {
            return PartialView("_ShopBackInit", "");
        }
        public ActionResult ShopBack(Agendamento agendamento)
        {
            ShopBackTagModel model = new ShopBackTagModel();

            if (agendamento != null)
            {
                model.idVenda = agendamento.CodigoAgendamento.ToString();
                model.Valor = agendamento.Valor.ToString();
            }
            else
            {
                model.idVenda = "0";
                model.Valor = "0";
            }

            return PartialView("_ShopBack", model);
        }
        #endregion

        #region ActionPay Tags        
        public ActionResult ActionPayInit()
        {
            return PartialView("_ActionPayInit", "");
        }
        public ActionResult ActionPay(Agendamento agendamento)
        {
            ActionPayTagModel model = new ActionPayTagModel();

            if (agendamento != null)
            {
                model.idVenda = agendamento.CodigoAgendamento.ToString();
                model.Valor = agendamento.Valor.ToString().Replace(",", ".");
            }
            else
            {
                model.idVenda = "0";
                model.Valor = "0";
            }

            return PartialView("_ActionPay", model);
        }
        #endregion

        #region Criteo Tags        
        public ActionResult CriteoInit()
        {
            return PartialView("_CriteoInit", "");
        }
        public ActionResult Criteo(Agendamento agendamento)
        {
            // ##TODO### Levar o código para a view
            string emailHash = "";
            if (!string.IsNullOrEmpty(agendamento.EmailDoCliente))
            {
                emailHash = CalculateMD5Hash(agendamento.EmailDoCliente.ToString());
            }

            var tagcriteo = new StringBuilder();
            tagcriteo.AppendLine(" <script type='text/javascript' src='//static.criteo.net/js/ld/ld.js' async='true'></script>");
            tagcriteo.AppendLine(" <script type='text/javascript'>");
            tagcriteo.AppendLine(" window.criteo_q = window.criteo_q || [];");
            tagcriteo.AppendLine(" window.criteo_q.push(  ");
            tagcriteo.AppendLine("{ event: \"setHashedEmail\", email: \"" + emailHash + "\" },");
            tagcriteo.AppendLine("{ event:\"setAccount\", account: 16342 },");
            tagcriteo.AppendLine("{ event: \"setSiteType\", type: \"d\" }, ");
            tagcriteo.AppendLine("{ event:\"trackTransaction\" , id: \"" + agendamento.CodigoAgendamento + "\", item: [ ");
            foreach (var item in agendamento.ItensDoAgendamento)
            {
                //int idcategoria = (int)Aspect.Context<Transaction, int>(aspect => DependencyInjectionFactory.Create<IProdutoCategoriaRepository>().ObterPorIdProduto(item.idProduto));

                tagcriteo.AppendLine("{ id: \"" + item.CodigoServico + "\", price: \"" + item.Valor.ToString().Replace(",", ".") + "\", quantity: \"" + item.QtdePeriodos + "\" },");
            }

            tagcriteo.AppendLine("]});");
            tagcriteo.AppendLine("</script>");

            ViewData["Message"] = tagcriteo.ToString();
            return PartialView();
        }
        #endregion

        #region Facebook Tags        
        public ActionResult FacebookLeads(Agendamento agendamento)
        {
            // ##TODO## Levar o código para a view
            StringBuilder facebookConversionCodeLeads = new StringBuilder();
            facebookConversionCodeLeads.AppendLine("<!-- Facebook Conversion Code for Leads Facebook -->");
            facebookConversionCodeLeads.AppendLine("<script>(function() {");
            facebookConversionCodeLeads.AppendLine("  var _fbq = window._fbq || (window._fbq = []);");
            facebookConversionCodeLeads.AppendLine("  if (!_fbq.loaded) {");
            facebookConversionCodeLeads.AppendLine("    var fbds = document.createElement('script');");
            facebookConversionCodeLeads.AppendLine("    fbds.async = true;");
            facebookConversionCodeLeads.AppendLine("    fbds.src = '//connect.facebook.net/en_US/fbds.js';");
            facebookConversionCodeLeads.AppendLine("    var s = document.getElementsByTagName('script')[0];");
            facebookConversionCodeLeads.AppendLine("    s.parentNode.insertBefore(fbds, s);");
            facebookConversionCodeLeads.AppendLine("    _fbq.loaded = true;");
            facebookConversionCodeLeads.AppendLine("  }");
            facebookConversionCodeLeads.AppendLine("})();");
            facebookConversionCodeLeads.AppendLine("window._fbq = window._fbq || [];");
            facebookConversionCodeLeads.AppendLine("window._fbq.push(['track', '601854743577', {'value':'" + agendamento.Valor.ToString() + "','currency':'BRL'}]);");
            facebookConversionCodeLeads.AppendLine("</script>");
            facebookConversionCodeLeads.AppendLine("<noscript><img height=\"1\" width=\"1\" alt=\"\" style=\"display:none\" src=\"https://www.facebook.com/tr?ev=601854743577&amp;cd[value]=" + agendamento.Valor.ToString() + "&amp;cd[currency]=BRL&amp;noscript=1\" /></noscript>");

            ViewData["Message"] = facebookConversionCodeLeads.ToString();
            return PartialView();
        }
        public ActionResult FacebookAbandono(Agendamento agendamento)
        {
            // ##TODO## Levar o código para a view
            StringBuilder facebookConversionCodeAbandono = new StringBuilder();
            facebookConversionCodeAbandono.AppendLine("<!-- Facebook Conversion Code for Abandono de Carrinho -->");
            facebookConversionCodeAbandono.AppendLine("<script>(function() {");
            facebookConversionCodeAbandono.AppendLine("  var _fbq = window._fbq || (window._fbq = []);");
            facebookConversionCodeAbandono.AppendLine("  if (!_fbq.loaded) {");
            facebookConversionCodeAbandono.AppendLine("    var fbds = document.createElement('script');");
            facebookConversionCodeAbandono.AppendLine("    fbds.async = true;");
            facebookConversionCodeAbandono.AppendLine("    fbds.src = '//connect.facebook.net/en_US/fbds.js';");
            facebookConversionCodeAbandono.AppendLine("    var s = document.getElementsByTagName('script')[0];");
            facebookConversionCodeAbandono.AppendLine("    s.parentNode.insertBefore(fbds, s);");
            facebookConversionCodeAbandono.AppendLine("    _fbq.loaded = true;");
            facebookConversionCodeAbandono.AppendLine("  }");
            facebookConversionCodeAbandono.AppendLine("})();");
            facebookConversionCodeAbandono.AppendLine("window._fbq = window._fbq || [];");
            facebookConversionCodeAbandono.AppendLine("window._fbq.push(['track', '6018547083971', {'value':'" + agendamento.Valor.ToString().Replace(",", ".") + "','currency':'BRL'}]);");
            facebookConversionCodeAbandono.AppendLine("</script>");
            facebookConversionCodeAbandono.AppendLine("<noscript><img height=\"1\" width=\"1\" alt=\"\" style=\"display:none\" src=\"https://www.facebook.com/tr?ev=6018547083971&amp;cd[value]=" + agendamento.Valor.ToString() + "&amp;cd[currency]=BRL&amp;noscript=1\" /></noscript>");

            ViewData["Message"] = facebookConversionCodeAbandono.ToString();
            return PartialView();
        }
        public ActionResult FacebookCompra(Agendamento agendamento)
        {
            // ##TODO## Levar o código para a view
            StringBuilder facebookConversionCodeCompra = new StringBuilder();
            facebookConversionCodeCompra.AppendLine("<!-- Facebook Conversion Code for Compra Facebook -->");
            facebookConversionCodeCompra.AppendLine("<script language='javascript' type='text/javascript'>(function() {");
            facebookConversionCodeCompra.AppendLine("  var _fbq = window._fbq || (window._fbq = []);");
            facebookConversionCodeCompra.AppendLine("  if (!_fbq.loaded) {");
            facebookConversionCodeCompra.AppendLine("    var fbds = document.createElement('script');");
            facebookConversionCodeCompra.AppendLine("    fbds.async = true;");
            facebookConversionCodeCompra.AppendLine("    fbds.src = '//connect.facebook.net/en_US/fbds.js';");
            facebookConversionCodeCompra.AppendLine("    var s = document.getElementsByTagName('script')[0];");
            facebookConversionCodeCompra.AppendLine("    s.parentNode.insertBefore(fbds, s);");
            facebookConversionCodeCompra.AppendLine("    _fbq.loaded = true;");
            facebookConversionCodeCompra.AppendLine("  }");
            facebookConversionCodeCompra.AppendLine("})();");
            facebookConversionCodeCompra.AppendLine("window._fbq = window._fbq || [];");
            facebookConversionCodeCompra.AppendLine("window._fbq.push(['track', '6018547055571', {'value':'" + agendamento.Valor.ToString().Replace(",", ".") + "','currency':'BRL'}]);");
            facebookConversionCodeCompra.AppendLine("</script>");
            facebookConversionCodeCompra.AppendLine("<noscript><img height=\"1\" width=\"1\" alt=\"\" style=\"display:none\" src=\"https://www.facebook.com/tr?ev=6018547055571&amp;cd[value]=" + agendamento.Valor.ToString() + "&amp;cd[currency]=BRL&amp;noscript=1\" /></noscript>");

            ViewData["Message"] = facebookConversionCodeCompra.ToString();
            return PartialView();
        }
        public ActionResult FacebookRemarketing()
        {
            return PartialView("_FacebookRemarketing", "");
        }
        public ActionResult FacebookSDK()
        {
            return PartialView("_FacebookSDK", "");
        }
        #endregion

        public ActionResult Afilio(Agendamento agendamento)
        {
            // ##TODO## Levar o código para a view
            ViewData["Message"] = "<img src='https://secure.afilio.com.br/sale.php?pid=1171&order_id=" + agendamento.CodigoAgendamento + "&order_price=" + agendamento.Valor.ToString().Replace(".", "").Replace(",", "") + "&preurl=' border='0' width='1' height='1' />";
            return PartialView();
        }

        public ActionResult Zopim()
        {
            return PartialView("_Zopim", "");
        }

        public ActionResult Optimizely()
        {
            return PartialView("_Optimizely", "");
        }

        public ActionResult CrazyEgg()
        {
            return PartialView("_CrazyEgg", "");
        }

        public ActionResult AlexaInit()
        {
            return PartialView("_AlexaInit", "");
        }

        public ActionResult DigiCertInit()
        {
            return PartialView("_DigiCertInit", "");
        }

        #region Métodos Privados        
        private string CalculateMD5Hash(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
        #endregion


    }
}