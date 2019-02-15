using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using SpaPlum.Web.Service;
using System.Data.Entity.Validation;
using SpaPlum.Web.Helpers;
using System.Activities.Statements;
using System.Text;
using Newtonsoft.Json;
using SpaPlum.Web.Models;
using MessagingToolkit.QRCode.Codec;
using System.Drawing;
using System.Diagnostics;
using System.Web.Configuration;
using PagedList;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.IO;


namespace SpaPlum.Web.Controllers
{
	public class AgendamentoController : SpaPlumBaseController
	{
		private AgendamentoContexto db = new AgendamentoContexto();

		// GET: Agendamento
		//[Authorize]
		//public async Task<ActionResult> Index()
		//{
		//    var agendamentoModels = db.AgendamentoModels.Include(a => a.TipoPagamento).Include(a => a.Filial).Include(a => a.ItensDoAgendamento).OrderByDescending(a => a.DataInicial);
		//    return View(await agendamentoModels.ToListAsync());
		//}

		[Authorize]
		public ActionResult Index(int? page)
		{
			var agendamentoModels = db.AgendamentoModels.Include(a => a.TipoPagamento)
														.Include(a => a.Filial)
														.Include(a => a.ItensDoAgendamento)
														.OrderByDescending(a => a.DataInicial)
														.ToPagedList(page ?? 1, 10);
			return View(agendamentoModels);
		}

		// GET: Agendamento/Details/5
		[Authorize]
		public async Task<ActionResult> Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Agendamento agendamento = await db.AgendamentoModels.Include(a => a.TipoPagamento)
																.Include(a => a.Filial)
																.Include(a => a.ItensDoAgendamento)
																.Where(x => x.CodigoAgendamento == id)
																.FirstOrDefaultAsync(); ;
			if (agendamento == null)
			{
				return HttpNotFound("Opa! Não era para ser assim...");
			}
			return View(agendamento);
		}

		// GET: Agendamento/Create        
		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		public ActionResult Create()
		{
			CarregarMochilao();
			return View();
		}

		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult CreateWordpress()
		{
			CarregarMochilao();
			return View();
		}

		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult Wizard()
		{
			return View();
		}

		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult WizardPasso1()
		{
			ViewBag.ListaFilial = new SelectList(db.FilialModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome), "CodigoFilial", "Nome");

			return View();
		}

		[HttpPost]
		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult WizardPasso1(AgendamentoModel model)
		{
			return RedirectToAction("WizardPasso2", model);
		}

		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult WizardPasso2(AgendamentoModel model)
		{
			if (model.CodigoFilial == null || model.CodigoFilial <= 0)
			{
				ModelState.AddModelError("Filial não selecionada.\rRetorne ao passo anterior.", new Exception("Filial não selecionada.\rRetorne ao passo anterior."));
			}

			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult ActionWizardPasso2(AgendamentoModel model)
		{
			return RedirectToAction("WizardPasso3", model);
		}

		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult WizardPasso3(AgendamentoModel model)
		{
			if (model.CodigoFilial == null || model.CodigoFilial <= 0)
			{
				ModelState.AddModelError("Filial não selecionada.\rRetorne ao passo 1.", new Exception("Filial não selecionada.\rRetorne ao passo 1."));
			}

			if (model.DataInicial == null || model.DataInicial <= DateTime.Now)
			{
				ModelState.AddModelError("Data Inicial não selecionada.\rRetorne ao passo anterior.", new Exception("Data Inicial não selecionada.\rRetorne ao passo anterior."));
			}

			ViewBag.ListaServico = new SelectList(db.ServicoModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome), "CodigoServico", "Nome");
			ViewBag.ListaTerapeuta = new SelectList(db.TerapeutaModels.Where(x => x.Ativa == true).OrderBy(x => x.CodigoFilial).ThenBy(x => x.Nome), "CodigoTerapeuta", "Nome");
			var tipos = new Tipos().getTiposDeMassagens();
			var lista = new SelectList(tipos, "Id", "Name");
			lista.Where(x => x.Value == "0").FirstOrDefault().Selected = true;
			ViewBag.ListaTipoDeMassagem = lista;

			return View(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult ActionWizardPasso3(AgendamentoModel model)
		{
			var pacote = @ViewBag.Pacote;
			if (pacote != null)
			{
				foreach (var item in pacote)
				{
					model.ItensDoAgendamento.Add
						(
						new AgendamentoItemServico
						{
							CodigoServico = item.CodigoServico,
							CodigoTerapeuta = item.CodigoTerapeuta,
							QtdePeriodos = item.QtdePeriodos,
							TipoDeMassagem = item.TipoDeMassagem,
							Valor = item.Valor
						}
						);
				}
			}
			return RedirectToAction("WizardPasso4", model);
		}

		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult WizardPasso4(AgendamentoModel model)
		{
			ViewBag.ListaTipoPagamento = new SelectList(db.TipoPagamentoModels.Where(x => x.Ativo == true).OrderBy(x => x.Descricao), "CodigoTipoPagamento", "Descricao");

			return View(model);
		}

		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult WizardPasso5(AgendamentoModel model)
		{
			return View(model);
		}

		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult WizardRevisao(AgendamentoModel model)
		{
			Agendamento entidade = new Agendamento(); // converteModelParaEntidade(model);

			CarregarMochilao();


			return View(entidade);
		}

		[HttpGet]
		public async Task<ActionResult> Agendado(string auth)
		{
			db.Configuration.LazyLoadingEnabled = false;

			/*
            var agendamentoRetorno = await db.AgendamentoModels.Include(x => x.TipoPagamento)
                                                               .Include(x => x.Filial)
                                                               .Include(x => x.ItensDoAgendamento)                                                               
                                                               .Where(x => x.Autenticacao.Equals(auth)).FirstOrDefaultAsync();
            */
			var agendamentoRetorno = await (from agendamento in db.AgendamentoModels
											join itens in db.AgendamentoItemServicoModels.Include(t => t.Servico).Include(t => t.Terapeuta)
											on agendamento.CodigoAgendamento equals itens.CodigoAgendamento

											join servico in db.ServicoModels
											on itens.CodigoServico equals servico.CodigoServico

											join terapeuta in db.TerapeutaModels
											on itens.CodigoTerapeuta equals terapeuta.CodigoTerapeuta

											where agendamento.Autenticacao.Equals(auth)
											select agendamento)
									 .Include(x => x.TipoPagamento)
									 .Include(x => x.Filial)
									 .Include(x => x.ItensDoAgendamento)
									 .FirstOrDefaultAsync();

			if (agendamentoRetorno == null)
			{
				return HttpNotFound("Opa! Não era para ser assim...");
			}

			return View(agendamentoRetorno);
		}

		public async Task<ActionResult> Sucesso(Agendamento agendamento)
		{
			var agendamentoRetorno = await db.AgendamentoModels.FindAsync(agendamento.CodigoAgendamento);
			if (agendamentoRetorno == null)
			{
				return HttpNotFound("Opa! Não era para ser assim...");
			}

			return View(agendamentoRetorno);
		}

		public async Task<ActionResult> SucessoWordpress(Agendamento agendamento)
		{
			var agendamentoRetorno = await db.AgendamentoModels.FindAsync(agendamento.CodigoAgendamento);
			if (agendamentoRetorno == null)
			{
				return HttpNotFound("Opa! Não era para ser assim...");
			}

			return View(agendamentoRetorno);
		}

		[System.Obsolete("Submit event not exists in View('Create')")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Create([Bind(Include = "CodigoAgendamento,NomeCliente,ClubeVIP,DataInicial,DataFinal,Valor,ValorTaxaAdicional,CodigoServico,CodigoStatusAgendamento,CodigoTerapeuta,CodigoTipoPagamento,CodigoFilial,EmailDoCliente,TipoDeMassagem,QuantidadePeriodos,Observacao,ValorTaxaFormaPagto,ValorMoraFormaPagto,ValorEmpresa,ValorTerapeuta")] Agendamento agendamento)
		{
			CarregarMochilao();
			return View(agendamento);
		}

		[System.Obsolete("Submit event not exists in View('CreateWordpress')")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateWordpress([Bind(Include = "CodigoAgendamento,NomeCliente,ClubeVIP,DataInicial,DataFinal,Valor,ValorTaxaAdicional,CodigoServico,CodigoStatusAgendamento,CodigoTerapeuta,CodigoTipoPagamento,CodigoFilial,EmailDoCliente,TipoDeMassagem,QuantidadePeriodos,Observacao,ValorTaxaFormaPagto,ValorMoraFormaPagto,ValorEmpresa,ValorTerapeuta")] Agendamento agendamento)
		{
			CarregarMochilao();
			return View(agendamento);
		}

		// GET: Agendamento/Edit/5
		[Authorize]
		public async Task<ActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			Agendamento agendamento = await db.AgendamentoModels.FindAsync(id);

			if (agendamento == null)
			{
				return HttpNotFound();
			}
			CarregarMochilao();

			return View(agendamento);
		}


		[System.Obsolete("Submit event not exists in View('Edit')")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> Edit([Bind(Include = "CodigoAgendamento,CodigoFilial,NomeCliente,ClubeVIP,DataInicial,DataFinal,Valor,ValorTaxaAdicional,CodigoServico,CodigoStatusAgendamento,CodigoTerapeuta,CodigoTipoPagamento,EmailDoCliente,TipoDeMassagem,CodigoTipoDeMassagem,QuantidadePeriodos,Observacao,ValorTaxaFormaPagto,ValorMoraFormaPagto,ValorEmpresa,ValorTerapeuta")] Agendamento agendamento)
		{
			if (ModelState.IsValid)
			{

			}
			CarregarMochilao();
			return View(agendamento);
		}

		[Authorize]
		public ActionResult Cancelar(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			int idAgendamento = Convert.ToInt32(id);

			var agendamento = db.AgendamentoModels.AsNoTracking().Where(x => x.CodigoAgendamento == idAgendamento).FirstOrDefault();
			if (agendamento != null)
			{
				agendamento.CodigoStatusAgendamento = (int)StatusAgendamento.Cancelado;

				db.Entry(agendamento).State = System.Data.Entity.EntityState.Modified;
				db.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}

		[Authorize]
		public ActionResult Pago(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			int idAgendamento = Convert.ToInt32(id);

			var agendamento = db.AgendamentoModels.AsNoTracking().Where(x => x.CodigoAgendamento == idAgendamento).FirstOrDefault();
			if (agendamento != null)
			{
				agendamento.CodigoStatusAgendamento = (int)StatusAgendamento.Pago;

				db.Entry(agendamento).State = System.Data.Entity.EntityState.Modified;
				db.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}

		[Authorize]
		public ActionResult Confirmado(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			int idAgendamento = Convert.ToInt32(id);

			var agendamento = db.AgendamentoModels.AsNoTracking().Where(x => x.CodigoAgendamento == idAgendamento).FirstOrDefault();
			if (agendamento != null)
			{
				agendamento.CodigoStatusAgendamento = (int)StatusAgendamento.Confirmado;

				db.Entry(agendamento).State = System.Data.Entity.EntityState.Modified;
				db.SaveChangesAsync();
			}

			return RedirectToAction("Index");
		}

		// GET: Agendamento/Delete/5
		[Authorize]
		public async Task<ActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Agendamento agendamento = await db.AgendamentoModels.Include(a => a.TipoPagamento)
																.Include(a => a.Filial)
																.Include(a => a.ItensDoAgendamento)
																.Where(x => x.CodigoAgendamento == id)
																.FirstOrDefaultAsync();

			if (agendamento == null)
			{
				return HttpNotFound();
			}
			return View(agendamento);
		}

		// POST: Agendamento/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteConfirmed(int id)
		{
			try
			{
				Agendamento agendamento = await db.AgendamentoModels.Include(t => t.Filial)
																	.Include(t => t.TipoPagamento)
																	.Include(t => t.ItensDoAgendamento)
																	.Where(x => x.CodigoAgendamento == id).FirstOrDefaultAsync();
				foreach (var item in agendamento.ItensDoAgendamento)
				{
					db.AgendamentoItemServicoModels.Remove(item);
				}

				db.AgendamentoModels.Remove(agendamento);
				await db.SaveChangesAsync();
			}
			catch (DbEntityValidationException e)
			{
				string msg = "";
				foreach (var eve in e.EntityValidationErrors)
				{
					msg += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
						eve.Entry.Entity.GetType().Name, eve.Entry.State);

					foreach (var ve in eve.ValidationErrors)
					{
						msg += string.Format("- Property: \"{0}\", Error: \"{1}\"",
							ve.PropertyName, ve.ErrorMessage);
					}
				}
				throw new Exception("Falha na verificação de dados." + Environment.NewLine + msg);
			}

			return RedirectToAction("Index");
		}

		[Authorize]
		public ActionResult VerCalendario()
		{
			return View();
		}

		public ActionResult Backend()
		{
			return new Dpc().CallBack(this);
		}

		public PartialViewResult CarregarServicos(int id)
		{
			return PartialView(db.ServicoModels.Where(x => x.CodigoFilial == id).OrderBy(x => x.Nome));
		}

		[HttpPost]
		public ActionResult ServicosPorFilial(int codigoFilial)
		{
			if (codigoFilial > 0)
			{
				var servicos = (from s in db.ServicoModels
								where s.CodigoFilial == codigoFilial && s.Ativo == true
								select new
								{
									CodigoServico = s.CodigoServico
									,
									EmPromocao = s.EmPromocao
									,
									Nome = s.Nome
									,
									Valor = s.Valor
									,
									ValorPromocao = s.ValorPromocao
								}).ToArray();

				return Json(servicos);
			}
			else return Json("");
		}

		[HttpPost]
		public ActionResult TerapeutasPorFilial(int codigoFilial)
		{
			var terapeutas = (from s in db.TerapeutaModels
							  where s.CodigoFilial == codigoFilial && s.Ativa == true
							  select new
							  {
								  CodigoTerapeuta = s.CodigoTerapeuta
								  ,
								  Nome = s.Nome
							  }).ToArray();

			return Json(terapeutas);
		}

		[HttpPost]
		public ActionResult TerapeutasPorServicoFilialHorario(int codigoFilial, int codigoServico, string dataAgendamento)
		{
			if (codigoFilial > 0 && !string.IsNullOrEmpty(dataAgendamento.Trim()) && codigoServico > 0)
			{

				DateTime DataAgendamento = DateTime.Now;

				DateTime.TryParse(dataAgendamento, out DataAgendamento);

				string somenteData = dataAgendamento.Substring(6, 4) + '-' + dataAgendamento.Substring(3, 2) + '-' + dataAgendamento.Substring(0, 2);
				string dataComparativa = somenteData + dataAgendamento.Substring(10, dataAgendamento.Length - 10);

				// OBTEM SOMENTE AS TERAPEUTAS QUE ATENDEM NO HORARIO ESPECIFICADO
				StringBuilder sql = new StringBuilder();
				sql.AppendLine("select distinct(t.COD_TERAPEUTA)");
				sql.AppendLine("from TB_TERAPEUTA t");
				sql.AppendLine("          inner join TB_HORARIO_TERAPEUTA h");
				sql.AppendLine("              on h.COD_TERAPEUTA = t.COD_TERAPEUTA");
				sql.AppendLine("          inner join TB_TERAPEUTA_SERVICO s");
				sql.AppendLine("              on s.COD_TERAPEUTA = t.COD_TERAPEUTA");
				sql.AppendLine("   where '" + dataComparativa + "' >= concat('" + somenteData + "', ' ', h.HOR_HORA_INICIO)");
				sql.AppendLine("     and '" + dataComparativa + "' <= concat('" + somenteData + "', ' ', h.HOR_HORA_FIM)");
				sql.AppendLine("     and t.COD_FILIAL = " + codigoFilial);
				sql.AppendLine("     and h.COD_DIA_DA_SEMANA = " + (int)DataAgendamento.DayOfWeek);
				sql.AppendLine("     and s.COD_SERVICO = " + codigoServico.ToString());

				var k = db.Database.SqlQuery<int>(sql.ToString()).ToList();
				List<string> vetor = new List<string>();
				foreach (var item in k)
				{
					vetor.Add(item.ToString());
				}

				// VERIFICA SE ALGUMA TERAPEUTA TEM AGENDAMENTO NO HORARIO

				var agendamentosNoHorario = from agendamento in db.AgendamentoModels
											join itens in db.AgendamentoItemServicoModels
											on agendamento.CodigoAgendamento equals itens.CodigoAgendamento
											where agendamento.CodigoFilial == codigoFilial
											   && DataAgendamento >= agendamento.DataInicial
											   && DataAgendamento <= agendamento.DataFinal
											   && vetor.Contains(itens.Terapeuta.CodigoTerapeuta.ToString())
											select itens;


				List<string> vetor2 = new List<string>();
				foreach (var item in agendamentosNoHorario)
				{
					vetor2.Add(item.CodigoTerapeuta.ToString());
				}

				// CARREGA SOMENTE AS ATIVAS DA MESMA FILIAL QUE ESTA DENTRO DA JANELA DE HORARIO
				var terapeutas = (from s in db.TerapeutaModels.Include(t => t.Horarios).Include(t => t.Servicos)
								  where s.CodigoFilial == codigoFilial
										&& s.Ativa == true
										&& vetor.Contains(s.CodigoTerapeuta.ToString())    // QUE ESTA DENTRO DA JANELA DE HORARIO
										&& !vetor2.Contains(s.CodigoTerapeuta.ToString())  // QUE NAO POSSUI ATENDIMENTO NO HORARIO                                                                          
								  select s).ToArray();


				// TODO: SE FOR AGENDAMENTO PARA HOJE, É PRECISO VERIFICAR NO PONTO SE O FUNCIONARIO ESTÁ PRESENTE
				/*
				if (DataAgendamento.Date.Equals(DateTime.Now.Date))
				{
					var terapeutasComPontoHoje = (from s in db.PontoTerapeutaModels.Include(t => t.Terapeuta)
												  where s.CodigoTerapeuta in terapeutas
						and 
						select s).ToArray();

				}
				*/
				// CARREGA SOMENTE OS DADOS QUE SERÃO APRESENTADOS
				var retorno = (from s in terapeutas
							   select new
							   {
								   CodigoTerapeuta = s.CodigoTerapeuta,
								   Nome = s.Nome
							   }).ToArray();

				return Json(retorno);
			}
			else return Json("");
		}

		[HttpPost]
		public ActionResult ObterAgendamento(string id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Int64 idInteiro = 0;
			if (id != null)
			{
				Int64.TryParse(id, out idInteiro);
			}

			if (idInteiro > 0)
			{
				var agendamentos = (from s in db.AgendamentoModels.Include(t => t.Filial).Include(t => t.ItensDoAgendamento)
									where s.CodigoAgendamento == idInteiro
									select s).ToArray();

				//var cacheServicos = db.ServicoModels.ToList();
				//var cacheTerapeutas = db.TerapeutaModels.ToList();


				var retorno = (from s in db.AgendamentoModels.Include(t => t.Filial).Include(t => t.ItensDoAgendamento)
							   where s.CodigoAgendamento == idInteiro
							   select new
							   {
								   DataInicial = s.DataInicial,
								   CodigoFilial = s.CodigoFilial,
								   Valor = s.Valor,
								   CodigoTipoPagamento = s.CodigoTipoPagamento,
								   ValorTaxaAdicional = (s.ValorTaxaAdicional != null) ? s.ValorTaxaAdicional : 0,
								   NomeDoCliente = s.NomeCliente,
								   EmailDoCliente = s.EmailDoCliente,
								   TelefoneDoCliente = (s.TelefoneDoCliente != null) ? s.TelefoneDoCliente : "",
								   Observacao = s.Observacao,
								   CodigoStatusAgendamento = s.CodigoStatusAgendamento,
								   ItensDoAgendamento = (from k in s.ItensDoAgendamento
														 select new
														 {
															 CodigoServico = k.CodigoServico,
															 NomeServico = from r in db.ServicoModels.Where(o => o.CodigoServico == k.CodigoServico) select (r.Nome),
															 Valor = k.Valor,
															 TipoDeMassagem = k.TipoDeMassagem,
															 QtdePeriodos = k.QtdePeriodos,
															 CodigoTerapeuta = k.CodigoTerapeuta,
															 NomeTerapeuta = from r in db.TerapeutaModels.Where(o => o.CodigoTerapeuta == k.CodigoTerapeuta) select (r.Nome),
															 DescricaoTipoDeMassagem = ((k.TipoDeMassagem == 0) ? "Padrão" :
																						   ((k.TipoDeMassagem == 1) ? "4 Mãos" :
																							  ((k.TipoDeMassagem == 2) ? "Para Casais" : "")))
														 }).ToList()
							   }).ToArray();

				return Json(retorno);
			}
			else return Json("");
		}

		[HttpPost]
		public ActionResult ObterServicosDaFilial(string codigoFilial)
		{
			if (codigoFilial == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Int64 idInteiro = 0;
			if (codigoFilial != null)
			{
				Int64.TryParse(codigoFilial, out idInteiro);
			}

			if (idInteiro > 0)
			{
				var retorno = (from s in db.ServicoModels //.Include(e => e.Filial)
							   where s.CodigoFilial == idInteiro
							   select new
							   {
								   CodigoServico = s.CodigoServico,
								   Nome = s.Nome
							   }).ToArray();


				return Json(retorno);

			}
			else return Json("");
		}

		[HttpPost]
		public ActionResult ObterServicosDaFilialDaTerapeuta(string codigoFilial, string codigoTerapeuta)
		{
			if (codigoFilial == null || codigoTerapeuta == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}

			Int64 idInteiroTerapeuta = 0;
			if (codigoTerapeuta != null)
			{
				Int64.TryParse(codigoTerapeuta, out idInteiroTerapeuta);
			}

			if (idInteiroTerapeuta > 0)
			{
				var servicosDaTerapeuta = (from s in db.TerapeutaServicoModels.Include(e => e.Terapeuta).Include(e => e.Servico)
										   where s.CodigoTerapeuta == idInteiroTerapeuta
										   select new
										   {
											   CodigoServico = s.Servico.CodigoServico,
											   Nome = s.Servico.Nome
										   });

				Int64 idInteiroFilial = 0;
				if (codigoFilial != null)
				{
					Int64.TryParse(codigoFilial, out idInteiroFilial);
				}

				if (idInteiroFilial > 0)
				{

					var servicosDaFilial = (from s in db.ServicoModels //.Include(e => e.Filial)
											where s.CodigoFilial == idInteiroFilial
											select new
											{
												CodigoServico = s.CodigoServico,
												Nome = s.Nome,
												Selected = servicosDaTerapeuta.Any()
											});//.ToArray();				


					return Json(servicosDaFilial.ToArray());
				}
				else return Json("");
			}
			else return Json("");
		}

		[HttpPost]
		public ActionResult FormasDePagamentoPorFilial(int codigoFilial)
		{
			if (codigoFilial > 0)
			{

				var formasDePagamento = db.TipoPagamentoModels.Where(x => x.Ativo == true && x.CodigoFilial == codigoFilial).OrderBy(x => x.Descricao).ToList();

				var retorno = (from s in formasDePagamento
							   select new
							   {
								   CodigoTipoPagamento = s.CodigoTipoPagamento,
								   Descricao = s.Descricao
							   }).ToArray();

				return Json(retorno);
			}
			else return Json("");

		}

		[HttpPost]
		public ActionResult TerapeutasDisponiveisPorFilialHorario(int codigoFilial, string dataAgendamento)
		{
			if (codigoFilial > 0 && !string.IsNullOrEmpty(dataAgendamento.Trim()))
			{

				DateTime DataAgendamento = DateTime.Now;

				DateTime.TryParse(dataAgendamento, out DataAgendamento);

				string somenteData = dataAgendamento.Substring(6, 4) + '-' + dataAgendamento.Substring(3, 2) + '-' + dataAgendamento.Substring(0, 2);
				string dataComparativa = somenteData + dataAgendamento.Substring(10, dataAgendamento.Length - 10);

				// OBTEM SOMENTE AS TERAPEUTAS QUE ATENDEM NO HORARIO ESPECIFICADO
				StringBuilder sql = new StringBuilder();
				sql.AppendLine("select distinct(h.COD_TERAPEUTA)");
				sql.AppendLine("from TB_TERAPEUTA t");
				sql.AppendLine("          inner join TB_HORARIO_TERAPEUTA h");
				sql.AppendLine("              on h.COD_TERAPEUTA = t.COD_TERAPEUTA");
				sql.AppendLine("   where '" + dataComparativa + "' >= concat('" + somenteData + "', ' ', h.HOR_HORA_INICIO)");
				sql.AppendLine("     and '" + dataComparativa + "' <= concat('" + somenteData + "', ' ', h.HOR_HORA_FIM)");
				sql.AppendLine("     and t.COD_FILIAL = " + codigoFilial);
				sql.AppendLine("     and h.COD_DIA_DA_SEMANA = " + (int)DataAgendamento.DayOfWeek);

				var k = db.Database.SqlQuery<int>(sql.ToString()).ToList();
				List<string> vetor = new List<string>();
				foreach (var item in k)
				{
					vetor.Add(item.ToString());
				}

				// VERIFICA SE ALGUMA TERAPEUTA TEM AGENDAMENTO NO HORARIO

				var agendamentosNoHorario = from agendamento in db.AgendamentoModels
											join itens in db.AgendamentoItemServicoModels
											on agendamento.CodigoAgendamento equals itens.CodigoAgendamento
											where agendamento.CodigoFilial == codigoFilial
											   && DataAgendamento >= agendamento.DataInicial
											   && DataAgendamento <= agendamento.DataFinal
											   && vetor.Contains(itens.Terapeuta.CodigoTerapeuta.ToString())
											select itens;

				/*
                var agendamentosNoHorario = db.AgendamentoModels.Where(x => x.CodigoFilial == codigoFilial
                                               && DataAgendamento >= x.DataInicial
                                               && DataAgendamento <= x.DataFinal
                                               && vetor.Contains(x.ItensDoAgendamento.Select new { Codigo =  ( { y => y.CodigoTerapeuta }))).ToList();
                */

				List<string> vetor2 = new List<string>();
				foreach (var item in agendamentosNoHorario)
				{
					vetor2.Add(item.CodigoTerapeuta.ToString());
				}

				// CARREGA SOMENTE AS ATIVAS DA MESMA FILIAL QUE ESTA DENTRO DA JANELA DE HORARIO
				var terapeutas = (from s in db.TerapeutaModels.Include(t => t.Horarios)
								  where s.CodigoFilial == codigoFilial
										&& s.Ativa == true
										&& vetor.Contains(s.CodigoTerapeuta.ToString())    // QUE ESTA DENTRO DA JANELA DE HORARIO
										&& !vetor2.Contains(s.CodigoTerapeuta.ToString())  // QUE NAO POSSUI ATENDIMENTO NO HORARIO                                  
								  select s).ToArray();

				// CARREGA SOMENTE OS DADOS QUE SERÃO APRESENTADOS
				var retorno = (from s in terapeutas
							   select new
							   {
								   CodigoTerapeuta = s.CodigoTerapeuta,
								   Nome = s.Nome
							   }).ToArray();

				return Json(retorno);
			}
			else return Json("");
		}

		[HttpPost]
		public ActionResult ObtemPrecoServico(int codigoServico)
		{
			if (codigoServico > 0)
			{
				var servico = (from s in db.ServicoModels
							   where s.CodigoServico == codigoServico
							   select new
							   {
								   CodigoServico = s.CodigoServico
								   ,
								   EmPromocao = s.EmPromocao
								   ,
								   Nome = s.Nome
								   ,
								   Valor = s.Valor
								   ,
								   ValorPromocao = s.ValorPromocao
							   }).FirstOrDefault();

				return Json(servico);
			}
			else return Json("");
		}


		internal void CarregarMochilao()
		{
			ViewBag.ListaServico = new SelectList(db.ServicoModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome), "CodigoServico", "Nome");
			ViewBag.ListaTerapeuta = new SelectList(db.TerapeutaModels.Where(x => x.Ativa == true).OrderBy(x => x.CodigoFilial).ThenBy(x => x.Nome), "CodigoTerapeuta", "Nome");
			ViewBag.ListaTipoPagamento = new SelectList(db.TipoPagamentoModels.Where(x => x.Ativo == true).OrderBy(x => x.Descricao), "CodigoTipoPagamento", "Descricao");
			ViewBag.ListaFilial = new SelectList(db.FilialModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome), "CodigoFilial", "Nome");
			ViewBag.ListaStatusAgendamento = new ListaStatusAgendamento().StatusAgendamentoListItem;

			var tipos = new Tipos().getTiposDeMassagens();
			var lista = new SelectList(tipos, "Id", "Name");
			lista.Where(x => x.Value == "0").FirstOrDefault().Selected = true;
			ViewBag.ListaTipoDeMassagem = lista;

			ViewBag.HoraInicial = DateTime.Now;
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddDetails(AgendamentoItemServico itemAgendamento)
		{
			if (ModelState.IsValid)
			{
				db.AgendamentoItemServicoModels.Add(itemAgendamento);
				db.SaveChanges();

				return RedirectToAction("DetailsGridPartial", new { OrderId = itemAgendamento.CodigoAgendamento });
			}
			return View(itemAgendamento);
		}

		public ActionResult GravarItem(string Items)
		{
			AgendamentoItemServicoController model = JsonConvert.DeserializeObject<AgendamentoItemServicoController>(Items);

			if (model != null)
				return Json(true);
			else
				return Json(false);

		}

		public ActionResult Generate(string texto)
		{
			QRCodeEncoder qrCodecEncoder = new QRCodeEncoder();
			qrCodecEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;
			qrCodecEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;
			qrCodecEncoder.CharacterSet = "UTF-8";
			qrCodecEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
			qrCodecEncoder.QRCodeScale = 6;
			qrCodecEncoder.QRCodeVersion = 0;
			qrCodecEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;

			if (Debugger.IsAttached)
			{
				texto = "http://localhost:62216/Agendamento/Agendado?auth=" + texto;
			}
			else
			{
				texto = "http://central.corpuspa.com.br/Agendamento/Agendado?auth=" + texto;
			}

			Bitmap imageQRCode;
			String data = texto;
			imageQRCode = qrCodecEncoder.Encode(data);

			//imgOut.Image = imageQRCode;
			System.IO.Stream stream = new System.IO.MemoryStream();
			imageQRCode.Save(stream, System.Drawing.Imaging.ImageFormat.Gif);
			stream.Flush();

			byte[] m_Bytes = StreamHelper.ReadToEnd(stream);

			Response.AddHeader("Content-Disposition", "inline; filename=SpaPlumQRCode.gif");

			return File(m_Bytes, "image/gif");
		}


		/// <summary>
		/// MENSAGEM AO CLIENTE
		/// TODO: ANEXAR .CAL de COMPROMISSO PARA IMPORTAÇÃO EM CELULAR
		/// </summary>
		/// <param name="agendamento"></param>
		/// <returns></returns>
		private string GerarMensagem(Agendamento agendamento)
		{
			StringBuilder retorno = new StringBuilder();

			//Se conseguir fazer a geringonça da renderização da partial view para enviar o e-mail corretamente, usa essa linha abaixo
			//retorno.Append(RenderPartialViewToString("Agendado", agendamento));


			retorno.AppendLine("<h2>SpaPlum</h2>");
			retorno.AppendLine("<br/>");
			retorno.AppendLine("<img src='http://www.spaplum.com.br/Content/images/icon-success.png' width='50' height='50' alt='sucesso' />");
			retorno.AppendLine("<h4>O agendamento foi concluído com sucesso.</h4>");
			retorno.AppendLine("<h6>Nossa Central entrará em contato em breve.</h6>");
			retorno.AppendLine("<hr/>");

			retorno.AppendLine("<img src='http://www.spaplum.com.br/Agendamento/Generate?texto='" + agendamento.Autenticacao.ToString() + "' alt='SpaPlum QRCode' style='position:absolute;background:#fff;margin-left:660px;zoom:70%;' />");

			retorno.AppendLine("");
			retorno.AppendLine("<dl class='dl-horizontal'>");

			retorno.AppendLine("    <dt><b>Local</b></dt>");
			retorno.AppendLine("    <dd>" + agendamento.Filial.Nome.ToString() + "</dd>");

			retorno.AppendLine("    <dt>&nbsp;</dt>");
			retorno.AppendLine("    <dd>&nbsp;</dd>");

			retorno.AppendLine("    <dt><b>Início</b></dt>");
			retorno.AppendLine("    <dd>" + agendamento.DataInicial.ToString() + "</dd>");

			retorno.AppendLine("    <dt><b>Término</b></dt>");
			retorno.AppendLine("    <dd>" + agendamento.DataFinal.ToString() + "</dd>");

			retorno.AppendLine("    <dt>&nbsp;</dt>");
			retorno.AppendLine("    <dd>&nbsp;</dd>");

			retorno.AppendLine("    <dt><b>Forma de Pagamento</b></dt>");
			retorno.AppendLine("    <dd>" + agendamento.TipoPagamento.Descricao.ToString() + "</dd>");

			retorno.AppendLine("    <dt>&nbsp;</dt>");
			retorno.AppendLine("    <dd>&nbsp;</dd>");

			retorno.AppendLine("    <dt><b>Valor</b></dt>");
			retorno.AppendLine("    <dd>" + agendamento.Valor.ToString() + "</dd>");

			retorno.AppendLine("    <dt>&nbsp;</dt>");
			retorno.AppendLine("    <dd>&nbsp;</dd>");

			retorno.AppendLine("    <dt><b>Nome do Cliente</b></dt>");
			retorno.AppendLine("    <dd>" + agendamento.NomeCliente != null ? agendamento.NomeCliente.ToString() : "" + "</dd>");

			retorno.AppendLine("    <dt>&nbsp;</dt>");
			retorno.AppendLine("    <dd>&nbsp;</dd>");

			retorno.AppendLine("    <dt><b>E-Mail</b></dd>");
			retorno.AppendLine("    <dd>" + agendamento.EmailDoCliente != null ? agendamento.EmailDoCliente.ToString() : "" + "</dd>");

			retorno.AppendLine("    <dt>&nbsp;</dt>");
			retorno.AppendLine("    <dd>&nbsp;</dd>");

			retorno.AppendLine("    <dt><b>Observação</b></dt>");
			string obs = agendamento.Observacao != null ? agendamento.Observacao.ToString() : "";
			retorno.AppendLine("    <dd>" + obs + "</dd>");

			retorno.AppendLine("    <dt>&nbsp;</dt>");
			retorno.AppendLine("    <dd>&nbsp;</dd>");

			retorno.AppendLine("    <dt>Autenticação</dt>");
			retorno.AppendLine("    <dd><a href='http://www.spaplum.com.br/Agendamento/Agendado?auth=" + agendamento.Autenticacao.ToString() + "'>" + agendamento.Autenticacao.ToString() + "</a></dd>");

			retorno.AppendLine("</dl>");

			retorno.AppendLine("<hr>");

			retorno.AppendLine("<table class='table' style='zoom:80%; width:80% '>");
			retorno.AppendLine("  <tr>");
			retorno.AppendLine("     <th>Terapia</th>");
			retorno.AppendLine("     <th>Tipo</th>");
			retorno.AppendLine("     <th style='text -align:center;'>Períodos</th>");
			retorno.AppendLine("     <th>Terapeuta</th>");
			retorno.AppendLine("     <th style='text -align:right;'>Valor Unit.</th>");
			retorno.AppendLine("     <th style='text -align:right;'>Valor Total</th>");
			retorno.AppendLine("  </tr>");

			foreach (var item in agendamento.ItensDoAgendamento)
			{
				var servico = new SpaPlum.Web.Contexto.AgendamentoContexto().ServicoModels.Find(item.CodigoServico).Nome;
				var terapeuta = new SpaPlum.Web.Contexto.AgendamentoContexto().TerapeutaModels.Find(item.CodigoTerapeuta).Nome;
				var tipo = new SpaPlum.Web.Helpers.ListaTipoDeMassagem().Lista.Where(x => x.Value.Equals(item.TipoDeMassagem.ToString())).FirstOrDefault().Text;
				var fator = 1;
				if (item.TipoDeMassagem > 0)
				{
					fator = 2;
				}

				retorno.AppendLine("  <tr>");
				retorno.AppendLine("    <td>" + servico + "</td>");
				retorno.AppendLine("    <td>" + tipo + "</td>");
				retorno.AppendLine("    <td style='text-align:center;'>" + item.QtdePeriodos + "</td>");
				retorno.AppendLine("    <td>" + terapeuta + "</td>");
				retorno.AppendLine("    <td style='text-align:right;'>" + item.Valor + "</td>");
				retorno.AppendLine("    <td style='text-align:right;'>" + (item.Valor * item.QtdePeriodos * fator) + "</td>");
				retorno.AppendLine("    </tr>");
			}

			retorno.AppendLine("  <tr>");
			retorno.AppendLine("    <td>&nbsp;</td>");
			retorno.AppendLine("    <td>&nbsp;</td>");
			retorno.AppendLine("    <td>&nbsp;</td>");
			retorno.AppendLine("    <td>&nbsp;</td>");
			retorno.AppendLine("    <td>&nbsp;</td>");
			retorno.AppendLine("    <td style='text-align:right;'>" + agendamento.Valor.ToString() + "</td>");
			retorno.AppendLine("  </tr>");
			retorno.AppendLine("  </table>");
			// Inclui uma imagem de um pixel quadrado para identificar leitura da mensagem
			retorno.AppendLine(string.Format("<img src='https://www.spaplum.com.br/Pixels/EmailDeAgendamento/{0}'>", agendamento.CodigoAgendamento));

			return retorno.ToString();
		}

		/// <summary>
		/// MENSAGEM A CORPUS SPA
		/// TODO: ANEXAR .CAL de COMPROMISSO PARA IMPORTAÇÃO EM CELULAR
		/// </summary>
		/// <param name="agendamento"></param>
		/// <returns></returns>
		private bool EnviarEmail(Agendamento agendamento)
		{
			var agenda = db.AgendamentoModels.Where(a => a.CodigoAgendamento == agendamento.CodigoAgendamento)
											 .Include(a => a.Filial)
											 .Include(a => a.ItensDoAgendamento)
											 .Include(a => a.TipoPagamento).FirstOrDefault();

			try
			{
				string mensagem = GerarMensagem(agenda);

				string EMAIL_DESTINO = WebConfigurationManager.AppSettings["EmailPrincipalCorpusSPA"];

				var email = new Email();
				email.CodigoEmail = 0;
				email.Assunto = "AGENDAR";
				email.DataEnvio = DateTime.Now;
				email.DoEmail = agenda.EmailDoCliente;
				email.DoNome = agenda.NomeCliente;
				email.Extra = "";
				email.ParaEmail = EMAIL_DESTINO;
				email.ParaNome = "Central Corpus SPA";
				email.FoiLido = false;
				email.HouveFalha = false;
				email.CodigoAgendamento = agenda.CodigoAgendamento;
				email.Mensagem = mensagem;				

				db.EmailModels.Add(email);
				db.SaveChangesAsync();

				try
				{
					new SpaPlum.Web.Service.Mailer().EnviarEmailAgendamento(email);
				}
				catch (Exception ex)
				{
					email.HouveFalha = true;
					if (!string.IsNullOrEmpty(ex.Message))
					{
						email.Extra = ex.Message.ToString();
					}
					db.Entry(email).State = EntityState.Modified;
					db.SaveChangesAsync();
					new RegistroDeLogsService("~/SpaPlumLogs").RegistrarLog(ex);
				}
			}
			catch (Exception ex)
			{
				new RegistroDeLogsService("~/SpaPlumLogs").RegistrarLog(ex);
			}


			return true;
		}

		/// <summary>
		/// GRAVAR AGENDAMENTO
		/// </summary>
		/// <param name="model">AgendamentoModel</param>
		/// <returns></returns>
		[HttpPost]
		[AllowAnonymous]
		[AllowCrossSiteAttribute]
		[AllowCrossSite]
		public ActionResult GravarAgendamento(AgendamentoModel model)
		{
			bool status = false;
			int id = 0;
			string auth = "";
			string msg = "";

			StringBuilder sbOutServicos = new StringBuilder();
			StringBuilder sbOutTerapeutas = new StringBuilder();

			Lancamento entidadeContaReceber = null;

			if (ModelState.IsValid)
			{
				if (model.ItensDoAgendamento.Count() > 0)
				{
					//decimal valor = 0;
					decimal valorTotal = 0;
					decimal valorLiquido = 0;
					decimal valorEmpresa = 0;
					decimal valorTerapeuta = 0;
					decimal valorTaxaAdicional = model.ValorTaxaAdicional;

					// INICIO - CARGA DA FORMA DE PAGAMENTO e as TAXAS/MORAS
					TipoPagamento tipoParamento = db.TipoPagamentoModels.AsNoTracking().Where(x => x.CodigoTipoPagamento == model.CodigoTipoPagamento).FirstOrDefault();

					decimal valorTaxaFormaPagto = 0;
					switch (tipoParamento.TipoTaxa)
					{
						case "P":
							if (tipoParamento.Taxa > 0)
							{
								valorTaxaFormaPagto = Convert.ToDecimal(model.Valor) * (Convert.ToDecimal(tipoParamento.Taxa) / 100);
							}
							break;
						case "R":
							valorTaxaFormaPagto = Convert.ToDecimal(tipoParamento.Taxa);
							break;
					}

					decimal valorMoraFormaPagto = 0;
					switch (tipoParamento.TipoMora)
					{
						case "P":
							if (tipoParamento.Mora > 0)
							{
								valorMoraFormaPagto = Convert.ToDecimal(model.Valor) * (Convert.ToDecimal(tipoParamento.Mora) / 100);
							}
							break;
						case "R":
							valorMoraFormaPagto = Convert.ToDecimal(tipoParamento.Mora);
							break;
					}
					// INICIO - CARGA DA FORMA DE PAGAMENTO e as TAXAS/MORAS

					// INICIO - PRO-RATA DE TAXAS E MORAS DE PAGAMENTO
					decimal servicoTaxaProRata = 0;
					decimal restoServicoTaxaProRata = 0;
					decimal servicoMoraProRata = 0;
					decimal restoServicoMoraProRata = 0;


					if (model.ItensDoAgendamento.Count > 1)
					{
						servicoTaxaProRata = decimal.Round(valorTaxaFormaPagto / model.ItensDoAgendamento.Count, 2);
						servicoMoraProRata = decimal.Round(valorMoraFormaPagto / model.ItensDoAgendamento.Count, 2);

						restoServicoTaxaProRata = (servicoTaxaProRata * model.ItensDoAgendamento.Count) - valorTaxaFormaPagto;
						restoServicoTaxaProRata = (restoServicoTaxaProRata > 0) ? restoServicoTaxaProRata : restoServicoTaxaProRata * (-1);

						restoServicoMoraProRata = (servicoMoraProRata * model.ItensDoAgendamento.Count) - valorMoraFormaPagto;
						restoServicoMoraProRata = (restoServicoMoraProRata > 0) ? restoServicoMoraProRata : restoServicoMoraProRata * (-1);
					}
					// FIM - PRO-RATA DE TAXAS E MORAS DE PAGAMENTO

					// INICIO - CALCULA TOTAIS DE SERVICOS
					int itemID = 1;
					int duracaoTodosServicos = 0;
					int totalPontosVIPGanhos = 0;
					int itemNumero = 1;
					foreach (var item in model.ItensDoAgendamento)
					{
						itemNumero++;

						Servico servico = db.ServicoModels.AsNoTracking().Where(x => x.CodigoServico == item.CodigoServico).FirstOrDefault();
						Terapeuta terapeuta = db.TerapeutaModels.AsNoTracking().Where(x => x.CodigoTerapeuta == item.CodigoTerapeuta).FirstOrDefault();

						sbOutServicos.AppendLine(servico.Nome);
						sbOutTerapeutas.AppendLine(terapeuta.Nome);

						if (itemNumero > 1)
						{
							if (itemNumero != model.ItensDoAgendamento.Count)
							{
								sbOutServicos.Append(", ");
								sbOutTerapeutas.Append(", ");
							}
							else
							{
								sbOutServicos.Append(".");
								sbOutTerapeutas.Append(".");
							}
						}

						if (servico != null)
						{
							int tipoMassagem = item.TipoDeMassagem;
							int fator = 1;
							if (tipoMassagem > 0)
							{
								fator = 2;
							}

							int qtde = item.QtdePeriodos;
							decimal valorUnitario = (!servico.EmPromocao) ? (servico.Valor * fator) : (servico.ValorPromocao * fator);

							decimal valorTotalServico = (valorUnitario * qtde);

							if (itemID == 1)
							{
								item.ValorTaxaProRata = servicoTaxaProRata + restoServicoTaxaProRata;
								item.ValorMoraProRata = servicoMoraProRata + restoServicoMoraProRata;
							}
							else
							{
								item.ValorTaxaProRata = servicoTaxaProRata;
								item.ValorMoraProRata = servicoMoraProRata;
							}

							duracaoTodosServicos += servico.TempoAproximadoDoServico * qtde;

							valorTotal += valorTotalServico;

							totalPontosVIPGanhos += servico.PontosVip;

							valorEmpresa += (!servico.EmPromocao) ? (servico.ValorComissaoEmpresa) : (servico.ValorPromocaoComissaoEmpresa);
							valorTerapeuta += (!servico.EmPromocao) ? (servico.ValorComissaoTerapeuta) : (servico.ValorPromocaoComissaoTerapeuta);

							item.ValorComissaoEmpresa += (!servico.EmPromocao) ? (servico.ValorComissaoEmpresa) : (servico.ValorPromocaoComissaoEmpresa);
							item.ValorComissaoTerapeuta += (!servico.EmPromocao) ? (servico.ValorComissaoTerapeuta) : (servico.ValorPromocaoComissaoTerapeuta);

						}
						itemID++;
					}
					valorEmpresa = valorEmpresa - servicoTaxaProRata - servicoMoraProRata - restoServicoTaxaProRata;

					// FIM - CALCULA TOTAIS DE SERVICOS

					// INICIO - CALCULA A DATA/HORA FINAL
					model.DataFinal = model.DataInicial.AddMinutes(duracaoTodosServicos);
					// FIM - CALCULA A DATA/HORA FINAL

					valorLiquido = Convert.ToDecimal(valorTotal) - Convert.ToDecimal(valorTaxaFormaPagto)
																 - Convert.ToDecimal(valorMoraFormaPagto)
																 + Convert.ToDecimal(valorTaxaAdicional);

					// VERIFICA SE OS VALORES QUE VIERAM BATEM COM OS VALORES RECALCULADOS
					if (model.Valor.Equals(valorTotal))
					{
						if (!User.Identity.IsAuthenticated)
						{
							// Salva o cliente
							try
							{
								var cliente = db.ClienteModels.Where(x => x.Email == model.EmailDoCliente).FirstOrDefault();
								if (cliente == null)
								{
									// Cadastra o cliente
									cliente = new Cliente();
									cliente.Nome = model.NomeCliente;
									cliente.Email = model.EmailDoCliente;
									cliente.PontosVIP += totalPontosVIPGanhos;
									cliente.Senha = model.Senha;
									cliente.ConfirmarSenha = model.ConfirmarSenha;
									cliente.Ativo = true;

									db.ClienteModels.Add(cliente);
									db.SaveChanges();

									// Cadastra o cliente como usuário no sistema
									RegisterViewModel registro = new RegisterViewModel();
									registro.Nome = cliente.Nome;
									registro.Email = cliente.Email;
									registro.Password = cliente.Senha;
									registro.ConfirmPassword = cliente.ConfirmarSenha;
									registro.Sobrenome = cliente.Nome.Split(' ').Count() > 1 ? cliente.Nome.Split(' ')[1] : "";
									registro.CodigoCliente = cliente.CodigoCliente;
									registro.CodigoPerfil = 1;
									new AccountController().SimpleRegister(registro);

								}
								else
								{



								}
							}
							catch
							{ }
						}

						Agendamento agendamento = null;

						if (model.CodigoAgendamento == 0)
						{
							agendamento = new Agendamento
							{
								CodigoAgendamento = 0,

								DataInicial = model.DataInicial,
								DataFinal = model.DataFinal,
								CodigoFilial = model.CodigoFilial,

								EmailDoCliente = model.EmailDoCliente,
								NomeCliente = model.NomeCliente,
								Observacao = model.Observacao,
								TelefoneDoCliente = model.TelefoneDoCliente,
								ClubeVIP = "",

								Autenticacao = System.Guid.NewGuid().ToString(),
								IPdoCliente = new NetworkUtils().GetIpAddress(),
								SessionID = HttpContext.Session.SessionID,

								CodigoStatusAgendamento = model.CodigoStatusAgendamento,
								CodigoTipoPagamento = model.CodigoTipoPagamento,

								// Valores Recalculados
								Valor = valorTotal,
								ValorEmpresa = valorEmpresa,
								ValorTerapeuta = valorTerapeuta,
								ValorLiquido = valorLiquido,
								ValorMoraFormaPagto = valorMoraFormaPagto,
								ValorTaxaAdicional = valorTaxaAdicional,
								ValorTaxaFormaPagto = valorTaxaFormaPagto
							};

							foreach (var item in model.ItensDoAgendamento)
							{
								agendamento.ItensDoAgendamento.Add(item);
							}

							db.AgendamentoModels.Add(agendamento);

							/*
							 * GRAVA A CONTA A RECEBER
							 */
							var contaReceber = new Lancamento();
							string tituloContaReceber = "AGENDAMENTO " + agendamento.DataInicial.ToString() + " -> " + agendamento.NomeCliente;
							string descricaoContaReceber = "AGENDAMENTO:" + Environment.NewLine +
														   "   Data do Agendamento = " + DateTime.Now.ToString() + Environment.NewLine +
														   "   Agendado para " + agendamento.DataInicial.ToString() + " e " + agendamento.DataFinal.ToString() + Environment.NewLine +
														   Environment.NewLine +
														  "SERVIÇOS:" + Environment.NewLine
														  + sbOutServicos.ToString() + Environment.NewLine +
														  Environment.NewLine +
														   "PROFISSIONAIS:" + Environment.NewLine +
														   sbOutTerapeutas.ToString() +
														   Environment.NewLine;

							contaReceber.CodigoLancamento = 0;
							contaReceber.CodigoFilial = agendamento.CodigoFilial;
							contaReceber.CodigoTipoOperacao = (int)TipoOperacao.Credito;
							contaReceber.CodigoTipoPagamento = agendamento.TipoPagamento.CodigoTipoPagamento;
							contaReceber.DataLancamento = DateTime.Now;
							contaReceber.DataVencimento = agendamento.DataFinal;
							contaReceber.DataPagamento = null;
							contaReceber.Descricao = descricaoContaReceber;
							contaReceber.Titulo = tituloContaReceber;
							contaReceber.Valor = Convert.ToDecimal(agendamento.Valor);
							/*
							 * TODO: FICA PARA A PROXIMA ALTERACAO DE MODELO
							 * 
							contaReceber.ValorLiquido = Convert.ToDecimal(agendamento.ValorLiquido);
							contaReceber.ValorTaxaAdicional = Convert.ToDecimal(agendamento.ValorTaxaAdicional);
							contaReceber.ValorTaxaFormaPagto = Convert.ToDecimal(agendamento.ValorTaxaFormaPagto);
							contaReceber.ValorMoraFormaPagto = Convert.ToDecimal(agendamento.ValorMoraFormaPagto);
							contaReceber.ValorEmpresa = Convert.ToDecimal(agendamento.ValorEmpresa);
							contaReceber.ValorTerapeuta = Convert.ToDecimal(agendamento.ValorTerapeuta);
							*/
							contaReceber.ValorMulta = 0;
							db.LancamentoModels.Add(contaReceber);
							/*
							 * FIM DA GRAVACAO DA CONTA A RECEBER
							 */

							/* GRAVA TODO O PACOTE DE AGENDAMENTO */
							db.SaveChanges();

							/* NOTIFICA O E-MAIL DA EMPRESA DIZENDO QUE ENTROU UM PREENCHIMENTO DE AGENDA */
							EnviarEmail(agendamento);
						}
						else
						{
							agendamento = db.AgendamentoModels.Include(t => t.Filial)
																  .Include(t => t.TipoPagamento)
																  .Include(t => t.ItensDoAgendamento)
																  .Where(x => x.CodigoAgendamento == model.CodigoAgendamento)
																  .FirstOrDefault();

							agendamento.CodigoAgendamento = model.CodigoAgendamento;
							agendamento.DataInicial = model.DataInicial;
							agendamento.DataFinal = model.DataFinal;
							agendamento.CodigoFilial = model.CodigoFilial;

							agendamento.EmailDoCliente = model.EmailDoCliente;
							agendamento.NomeCliente = model.NomeCliente;
							agendamento.Observacao = model.Observacao;
							agendamento.TelefoneDoCliente = model.TelefoneDoCliente;
							agendamento.ClubeVIP = "";

							agendamento.Autenticacao = System.Guid.NewGuid().ToString();
							agendamento.IPdoCliente = new NetworkUtils().GetIpAddress();
							agendamento.SessionID = HttpContext.Session.SessionID;

							agendamento.CodigoStatusAgendamento = model.CodigoStatusAgendamento;
							agendamento.CodigoTipoPagamento = model.CodigoTipoPagamento;

							// Valores Recalculados
							agendamento.Valor = valorTotal;
							agendamento.ValorEmpresa = valorEmpresa;
							agendamento.ValorTerapeuta = valorTerapeuta;
							agendamento.ValorLiquido = valorLiquido;
							agendamento.ValorMoraFormaPagto = valorMoraFormaPagto;
							agendamento.ValorTaxaAdicional = valorTaxaAdicional;
							agendamento.ValorTaxaFormaPagto = valorTaxaFormaPagto;

							agendamento.ItensDoAgendamento.Clear();
							foreach (var item in model.ItensDoAgendamento)
							{
								agendamento.ItensDoAgendamento.Add(item);
							}

							db.Entry(agendamento).State = EntityState.Modified;

							/*
							 * PROCURA E ALTERA A CONTA A RECEBER
							 */
							entidadeContaReceber = db.LancamentoModels.AsNoTracking().Where(l => l.CodigoAgendamento == agendamento.CodigoAgendamento).FirstOrDefault();

							if (entidadeContaReceber == null)
							{
								/* SE ENTRAR AQUI É PORQUE NAO EXISTIA UMA CONTA A RECEBER E ISSO NAO EH BOM. PRECISA VERIFICAR. */
								entidadeContaReceber = new Lancamento();
								string tituloContaReceber = "AGENDAMENTO " + agendamento.DataInicial.ToString() + " -> " + agendamento.NomeCliente;
								string descricaoContaReceber = "AGENDAMENTO:" + Environment.NewLine +
															   "   Data do Agendamento = " + DateTime.Now.ToString() + Environment.NewLine +
															   "   Agendado para " + agendamento.DataInicial.ToString() + " e " + agendamento.DataFinal.ToString() + Environment.NewLine +
															   Environment.NewLine +
															  "SERVIÇOS:" + Environment.NewLine
															  + sbOutServicos.ToString() + Environment.NewLine +
															  Environment.NewLine +
															   "PROFISSIONAIS:" + Environment.NewLine +
															   sbOutTerapeutas.ToString() +
															   Environment.NewLine;

								entidadeContaReceber.CodigoLancamento = 0;
								entidadeContaReceber.CodigoFilial = agendamento.CodigoFilial;
								entidadeContaReceber.CodigoTipoOperacao = (int)TipoOperacao.Credito;
								entidadeContaReceber.CodigoTipoPagamento = agendamento.TipoPagamento.CodigoTipoPagamento;
								entidadeContaReceber.DataLancamento = DateTime.Now;
								entidadeContaReceber.DataVencimento = agendamento.DataFinal;
								entidadeContaReceber.DataPagamento = null;
								entidadeContaReceber.Descricao = descricaoContaReceber;
								entidadeContaReceber.Titulo = tituloContaReceber;
								entidadeContaReceber.Valor = Convert.ToDecimal(agendamento.Valor);
								entidadeContaReceber.ValorMulta = 0;
								/*
								 * TODO: FICA PARA A PROXIMA ALTERACAO DE MODELO
								 * 
								contaReceber.ValorLiquido = Convert.ToDecimal(agendamento.ValorLiquido);
								contaReceber.ValorTaxaAdicional = Convert.ToDecimal(agendamento.ValorTaxaAdicional);
								contaReceber.ValorTaxaFormaPagto = Convert.ToDecimal(agendamento.ValorTaxaFormaPagto);
								contaReceber.ValorMoraFormaPagto = Convert.ToDecimal(agendamento.ValorMoraFormaPagto);
								contaReceber.ValorEmpresa = Convert.ToDecimal(agendamento.ValorEmpresa);
								contaReceber.ValorTerapeuta = Convert.ToDecimal(agendamento.ValorTerapeuta);
								*/

								db.LancamentoModels.Add(entidadeContaReceber);
							}
							else
							{
								/* NO CASO NORMAL, SEMPRE DEVE CAIR AQUI NA EDICAO DE AGENDAMENTO */
								string tituloContaReceber = "AGENDAMENTO [ALTERACAO] " + agendamento.DataInicial.ToString() + " -> " + agendamento.NomeCliente;
								string descricaoContaReceber = "AGENDAMENTO:" + Environment.NewLine +
															   "   Data do Agendamento = " + DateTime.Now.ToString() + Environment.NewLine +
															   "   Agendado para " + agendamento.DataInicial.ToString() + " e " + agendamento.DataFinal.ToString() + Environment.NewLine +
															   Environment.NewLine +
															  "SERVIÇOS:" + Environment.NewLine
															  + sbOutServicos.ToString() + Environment.NewLine +
															  Environment.NewLine +
															   "PROFISSIONAIS:" + Environment.NewLine +
															   sbOutTerapeutas.ToString() +
															   Environment.NewLine;

								//entidadeContaReceber.CodigoAgendamento = agendamento.CodigoAgendamento;
								entidadeContaReceber.CodigoFilial = agendamento.CodigoFilial;
								entidadeContaReceber.CodigoTipoOperacao = (int)TipoOperacao.Credito;
								entidadeContaReceber.CodigoTipoPagamento = agendamento.TipoPagamento.CodigoTipoPagamento;
								entidadeContaReceber.DataVencimento = agendamento.DataFinal;
								entidadeContaReceber.Descricao = descricaoContaReceber;
								entidadeContaReceber.Titulo = tituloContaReceber;
								entidadeContaReceber.Valor = Convert.ToDecimal(agendamento.Valor);
								/*
								 * TODO: FICA PARA A PROXIMA ALTERACAO DE MODELO
								 * 
								contaReceber.ValorLiquido = Convert.ToDecimal(agendamento.ValorLiquido);
								contaReceber.ValorTaxaAdicional = Convert.ToDecimal(agendamento.ValorTaxaAdicional);
								contaReceber.ValorTaxaFormaPagto = Convert.ToDecimal(agendamento.ValorTaxaFormaPagto);
								contaReceber.ValorMoraFormaPagto = Convert.ToDecimal(agendamento.ValorMoraFormaPagto);
								contaReceber.ValorEmpresa = Convert.ToDecimal(agendamento.ValorEmpresa);
								contaReceber.ValorTerapeuta = Convert.ToDecimal(agendamento.ValorTerapeuta);
								*/
								db.Entry(entidadeContaReceber).State = EntityState.Modified;
								db.LancamentoModels.Add(entidadeContaReceber);
							}
							/*
							 * FIM DA GRAVACAO DA CONTA A RECEBER
							 */
							db.SaveChangesAsync();
						}

						/*
                         * TESTAR :: SE O STATUS FOR PAGO, incluir registro no lançamento
                         * 
                        if (agendamento.CodigoStatusAgendamento == (int)StatusAgendamento.Pago)
                        {
                            string descricao = "Terapia|Tipo|Períodos|Terapeuta|Valor Unit|Valor Total\n";

                            foreach (var item in agendamento.ItensDoAgendamento)
                            {
                                var servico = new SpaPlum.Web.Contexto.AgendamentoContexto().ServicoModels.Find(item.CodigoServico).Nome;
                                var terapeuta = new SpaPlum.Web.Contexto.AgendamentoContexto().TerapeutaModels.Find(item.CodigoTerapeuta).Nome;
                                var tipo = new SpaPlum.Web.Helpers.ListaTipoDeMassagem().Lista.Where(x => x.Value.Equals(item.TipoDeMassagem.ToString())).FirstOrDefault().Text;
                                var fator = 1;
                                if (item.TipoDeMassagem > 0)
                                {
                                    fator = 2;
                                }

                                descricao += servico + "|" + tipo + "|" + item.QtdePeriodos + "|" + item.Valor + "|" + (item.Valor * item.QtdePeriodos * fator) + "\n");
                            }

                            Lancamento lancto = new Lancamento();
                            lancto.CodigoFilial = agendamento.CodigoFilial;
                            lancto.CodigoTipoOperacao = (int)TipoOperacao.Credito;
                            lancto.CodigoTipoPagamento = agendamento.CodigoTipoPagamento;
                            lancto.DataLancamento = DateTime.Now;
                            lancto.DataPagamento = DateTime.Now;
                            lancto.Titulo = "[MASSAGEM PAGA] " + agendamento.NomeCliente;

                            lancto.Descricao = descricao;
                            lancto.Valor = Convert.ToDecimal(agendamento.ValorLiquido);

                            db.LancamentoModels.Add(lancto);
                            db.SaveChanges();

                        }
                        */

						id = agendamento.CodigoAgendamento;
						status = true;
						auth = agendamento.Autenticacao;
						msg = "";
					}
				}
			}
			else
			{
				id = 0;
				status = false;
				auth = "";
				msg = "";
			}

			return new JsonResult { Data = new { status = status, id = id, auth = auth, msg = msg } };
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}




	}
}
