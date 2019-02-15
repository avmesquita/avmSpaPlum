using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using SpaPlum.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SpaPlum.Web.Controllers
{

    public class RelatorioController : SpaPlumBaseController
    {

        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: Relatorio
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<ActionResult> FinanceiroMassagens()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.ListaFilial = new SelectList(db.FilialModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome), "CodigoFilial", "Nome");

            ViewBag.conteudo = "";

            return View();
        }

        [Authorize]
        public async Task<ActionResult> MassagensMaisPedidas()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			ViewBag.ListaFilial = new SelectList(db.FilialModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome), "CodigoFilial", "Nome");

            ViewBag.conteudo = "";

            return View();
        }

        public ActionResult DisponibilidadeDeTerapeutas()
        {
            string conteudo = "";

            var hoje = (int)DateTime.Now.DayOfWeek;

            var filiais = db.FilialModels.Where(x => x.Ativo == true).OrderBy(x => x.Nome).ToList();
            foreach (var filial in filiais)
            {
                var horarios = db.HorarioTerapeutaModels.Include(t => t.Terapeuta)
                                        .Where(x => x.Terapeuta.CodigoFilial == filial.CodigoFilial && x.CodigoDiaDaSemana == hoje)
                                        .OrderBy(x => x.Terapeuta.Nome)
                                        .ToList();

                if (horarios.Count() > 0)
                {


                    conteudo += "<h3>" + filial.Nome + "</h3>";
                    conteudo += "<table class='table' style='width:80%'>";
                    conteudo += "  <tr>";
                    conteudo += "    <th>Terapeuta</th>";
                    conteudo += "    <th>Horarios</th>";
                    conteudo += "  </tr>";

                    string conteudoHorario = "";
                    int terapeutaAnterior = 0;
                    foreach (var horario in horarios)
                    {
                        if (terapeutaAnterior != horario.CodigoTerapeuta)
                        {
                            terapeutaAnterior = horario.CodigoTerapeuta;
                        }
                        conteudoHorario += "  <tr>";
                        conteudoHorario += "    <td>" + ((terapeutaAnterior == horario.CodigoTerapeuta) ? horario.Terapeuta.Nome : "&nbsp;") + "</td>";
                        conteudoHorario += "    <td>" + horario.HorarioInicio + " às " + horario.HorarioFim + "</td>";
                        conteudoHorario += "  </tr>";

                    }
                    conteudo += conteudoHorario + "</table>";
                    conteudo += "<br />";
                }
            }

            ViewBag.conteudo = conteudo;

            return View();
        }

        public ActionResult GerarTabelaDePrecos()
        {
            string conteudo = "";


            var filiais = db.FilialModels.Include(x => x.Servicos).Where(x => x.Ativo == true).OrderBy(x => x.Nome).ToList();
            foreach (var filial in filiais)
            {

                if (filial.Servicos.Count() > 0)
                {
                    conteudo += "<h3>" + filial.Nome + "</h3>";
                    conteudo += "<table class='table' style='width:80%'>";
                    conteudo += "  <tr>";
                    conteudo += "    <th>Serviço</th>";
                    conteudo += "    <th>Preço</th>";
                    conteudo += "  </tr>";

                    //var servicos = db.ServicoModels.Where(y => y.CodigoFilial == filial.CodigoFilial).ToList();
                    foreach (var servico in filial.Servicos)
                    {
                        conteudo += "  <tr>";
                        conteudo += "    <td>" + servico.Nome + "</td>";
                        conteudo += "    <td>" + ((!servico.EmPromocao)?servico.Valor.ToString():servico.ValorPromocao.ToString()) + "</td>";
                        conteudo += "  </tr>";

                    }
                    conteudo += "</table>";
                    conteudo += "<br />";
                }
            }

            ViewBag.conteudo = conteudo;

            return View();
        }

        [Authorize]
        public ActionResult GerarRelatorioHorariosAtendimento()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var horarioTerapeutas = db.HorarioTerapeutaModels.OrderBy(x => x.CodigoTerapeuta).ThenBy(x => x.CodigoDiaDaSemana).ThenBy(x => x.HorarioInicio).Include(h => h.Terapeuta);

            ViewBag.conteudo = "";

            return View(horarioTerapeutas.ToList());
        }


        [Authorize]
        public ActionResult GerarRelatorioHorarioAgrupado()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			// relatorio nao funcionou. refazer
			var horarioAgrupado = db.HorarioTerapeutaModels.Include(h => h.Terapeuta).GroupBy(x => x.CodigoDiaDaSemana);

            ViewBag.conteudo = "";

            return View(horarioAgrupado.ToList());
        }

        public ActionResult GerarRelatorioMassagensMaisPedidas(FiltroAgendamentoViewModel filtro)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			DateTime DataInicial = DateTime.Now;
            DateTime DataFinal = DateTime.Now;

            DateTime.TryParse(filtro.DataInicial, out DataInicial);
            DateTime.TryParse(filtro.DataFinal, out DataFinal);

            StringBuilder relatorio = new StringBuilder();
            #region Cabeçalho

            relatorio.AppendLine("<BR><BR><HR>");

            relatorio.AppendLine("<H2>Relatório Totalizador de Massagens</H2");
            relatorio.AppendLine(string.Format("<H4>De: {0} - Até: {1}</H4>", DataInicial.ToString("dd/MM/yyyy"), DataFinal.ToString("dd/MM/yyyy")));
            relatorio.AppendLine("<HR><BR>");
            #endregion

            var fakeFinal = DataFinal.AddDays(1);

            var agendamentos = db.AgendamentoModels
                .Where(x => x.CodigoFilial == filtro.CodigoFilial && x.DataInicial >= DataInicial && x.DataFinal <= fakeFinal)
                .Include(a => a.TipoPagamento)
                .Include(a => a.Filial)
                .Include(a => a.ItensDoAgendamento)
                .OrderBy(a => a.DataInicial);


            List<int> agendamentoListaCodigo = new List<int>();
            if (agendamentos.Count() > 0)
            {
                List<int> servicos = new List<int>();
                foreach (var item in agendamentos)
                {
                    foreach (var itemAgendamento in item.ItensDoAgendamento)
                    {
                        if (!servicos.Contains(itemAgendamento.CodigoServico))
                        {
                            servicos.Add(itemAgendamento.CodigoServico);
                        }
                    }

                    if (!agendamentoListaCodigo.Contains(item.CodigoAgendamento))
                    {
                        agendamentoListaCodigo.Add(item.CodigoAgendamento);
                    }
                }

                relatorio.AppendLine("<TABLE id='massagens-mais-pedidas' class='table' width='100%'>");
                relatorio.AppendLine("<thead><TR>");
                relatorio.AppendLine("  <TH><b>Masssagem</b></TH>");
                relatorio.AppendLine("  <TH><b>Qtde no Período</b></TH>");
                relatorio.AppendLine("</TR></thead><tbody>");
                foreach (var item in servicos)
                {
                    var servico = db.ServicoModels.AsNoTracking().Where(x => x.CodigoServico == item).FirstOrDefault();

                    var total = db.AgendamentoItemServicoModels.AsNoTracking()
                                                               .Where(x => x.CodigoServico.Equals(item) && agendamentoListaCodigo.Contains(x.CodigoAgendamento))
                                                               .Count();

                    relatorio.AppendLine("<TR>");
                    relatorio.AppendLine(string.Format("  <TD>{0}</TD>", servico.Nome));
                    relatorio.AppendLine(string.Format("  <TD>{0}</TD>", total));
                    relatorio.AppendLine("</TR>");
                }
                relatorio.AppendLine("</tbody></TABLE>");

                relatorio.AppendLine("<BR /><hr /><BR />");
            }
            else
            {
                relatorio.AppendLine("Não houve retorno na busca.");

                relatorio.AppendLine("<BR /><hr /><BR />");
            }

            ViewBag.relatorio = relatorio.ToString();

            return PartialView();
        }


        //[HttpPost]
        public ActionResult GerarRelatorio(FiltroAgendamentoViewModel filtro)
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			DateTime DataInicial = DateTime.Now;
            DateTime DataFinal = DateTime.Now;

            DateTime.TryParse(filtro.DataInicial, out DataInicial);
            DateTime.TryParse(filtro.DataFinal, out DataFinal);

            StringBuilder relatorio = new StringBuilder();
            #region Cabeçalho

            relatorio.AppendLine("<BR><BR><HR>");

            relatorio.AppendLine("<H2>Relatório de Massagens por Terapeutas</H2");
            relatorio.AppendLine(string.Format("<H4>De: {0} - Até: {1}</H4>", DataInicial.ToString("dd/MM/yyyy"), DataFinal.ToString("dd/MM/yyyy")));
            relatorio.AppendLine("<HR><BR>");
            #endregion


            Decimal valorTotalGeral = 0;
            Decimal valorTotalTaxaGeral = 0;

            Decimal valorTotalTerapeutaGeral = 0;
            Decimal valorTotalEmpresaGeral = 0;
            Decimal valorTotalTaxaPagtoGeral = 0;
            Decimal valorTotalMoraPagtoGeral = 0;


            var fakeFinal = DataFinal.AddDays(1);

            var agendamentos = db.AgendamentoModels
                .Where(x => x.CodigoFilial == filtro.CodigoFilial && x.DataInicial >= DataInicial && x.DataFinal <= fakeFinal)
                .Include(a => a.TipoPagamento)
                .Include(a => a.Filial)
                .Include(a => a.ItensDoAgendamento)
                .OrderBy(a => a.DataInicial);

            List<int> terapeutas = new List<int>();
            foreach (var item in agendamentos)
            {
                foreach (var itemAgendamento in item.ItensDoAgendamento)
                {
                    if (!terapeutas.Contains(itemAgendamento.CodigoTerapeuta))
                    {
                        terapeutas.Add(itemAgendamento.CodigoTerapeuta);
                    }
                }
            }

            // Carrega as terapeutas uma a uma
            foreach (var item in terapeutas)
            {
                var cacheTipoPagamento = db.TipoPagamentoModels.ToList();

                var terapeuta = db.TerapeutaModels.Include(t => t.Horarios)
                                                  .AsNoTracking()
                                                  .Where(x => x.CodigoTerapeuta == item).FirstOrDefault();

                relatorio.AppendLine("<H3>");
                relatorio.AppendLine(terapeuta.Nome);
                relatorio.AppendLine("</H3>");

                var agendamentosDaTerapeuta = db.AgendamentoItemServicoModels
                                                .Include(t => t.Agendamento)
                                                .Include(t => t.Servico)
                                                .Include(t => t.Terapeuta)
                                                .AsNoTracking().Where(x => x.CodigoTerapeuta == item);

                //var agendamentosDaTerapeuta = agendamentos.AsNoTracking().Where(x => x.CodigoTerapeuta == terapeuta.CodigoTerapeuta);                

                relatorio.AppendLine("<TABLE id='relatorio' class='table' width='100%'>");
                relatorio.AppendLine("<thead><TR>");
                relatorio.AppendLine("  <TH><b>Cliente</b></TH>");
                relatorio.AppendLine("  <TH><b>Inicio</b></TH>");
                relatorio.AppendLine("  <TH><b>Fim</b></TH>");
                relatorio.AppendLine("  <TH><b>Terapia</b></TH>");
                relatorio.AppendLine("  <TH><b>Tipo</b></TH>");
                relatorio.AppendLine("  <TH><b>Pagto</b></TH>");
                relatorio.AppendLine("  <TH><b>Status</b></TH>");
                relatorio.AppendLine("  <TH><b>Valor</b></TH>");
                relatorio.AppendLine("  <TH><b>Taxa AD.</b></TH>");
                relatorio.AppendLine("  <TH><b>Empresa</b></TH>");
                relatorio.AppendLine("  <TH><b>Terapeuta</b></TH>");
                relatorio.AppendLine("  <TH><b>Taxa Pagto</b></TH>");
                relatorio.AppendLine("  <TH><b>Mora Pagto</b></TH>");
                relatorio.AppendLine("</TR><thead><tbody>");

                Decimal valorTotal = 0;
                Decimal valorTotalTaxa = 0;
                Decimal valorTotalLiquido = 0;
                Decimal valorTotalTerapeuta = 0;
                Decimal valorTotalEmpresa = 0;

                Decimal valorTotalTaxaPagto = 0;
                Decimal valorTotalMoraPagto = 0;

                foreach (var agenda in agendamentosDaTerapeuta)
                {
                    string tipoMassagem = "";
                    switch (agenda.TipoDeMassagem)
                    {
                        case 0:
                            tipoMassagem = "Massagem Padrão";
                            break;
                        case 1:
                            tipoMassagem = "Massagem 4 Mãos";
                            break;
                        case 2:
                            tipoMassagem = "Massagem Para Casais";
                            break;
                    }


                    Decimal valorEmpresaNoServico = Convert.ToDecimal(agenda.ValorComissaoEmpresa)
                                                    - Convert.ToDecimal(agenda.ValorTaxaProRata)
                                                    - Convert.ToDecimal(agenda.ValorMoraProRata);

                    relatorio.AppendLine("<TR>");
                    relatorio.AppendLine("  <TD>" + agenda.Agendamento.NomeCliente + "</TD>");
                    relatorio.AppendLine("  <TD>" + agenda.Agendamento.DataInicial + "</TD>");
                    relatorio.AppendLine("  <TD>" + agenda.Agendamento.DataFinal + "</TD>");
                    relatorio.AppendLine("  <TD>" + agenda.Servico.Nome + "</TD>");
                    relatorio.AppendLine("  <TD>" + tipoMassagem + "</TD>");
                    relatorio.AppendLine("  <TD>" + cacheTipoPagamento.Where(x => x.CodigoTipoPagamento.Equals(agenda.Agendamento.CodigoTipoPagamento)).FirstOrDefault().Descricao + "</TD>");
                    relatorio.AppendLine("  <TD>" + @Enum.GetName(typeof(SpaPlum.Web.Entity.StatusAgendamento), agenda.Agendamento.CodigoStatusAgendamento) + "</TD>");
                    relatorio.AppendLine("  <TD style='text-align:right;'>" + agenda.Valor.ToString() + "</TD>");
                    relatorio.AppendLine("  <TD style='text-align:right;'>" + agenda.Agendamento.ValorTaxaAdicional.ToString() + "</TD>");
                    relatorio.AppendLine("  <TD style='text-align:right;'>" + valorEmpresaNoServico.ToString() + "</TD>");
                    relatorio.AppendLine("  <TD style='text-align:right;'>" + agenda.ValorComissaoTerapeuta.ToString() + "</TD>");
                    relatorio.AppendLine("  <TD style='text-align:right;'>" + agenda.ValorTaxaProRata.ToString() + "</TD>");
                    relatorio.AppendLine("  <TD style='text-align:right;'>" + agenda.ValorMoraProRata.ToString() + "</TD>");

                    relatorio.AppendLine("</TR>");

                    // TOTALIZADOR POR TERAPEUTA
                    valorTotal += 0 + Convert.ToDecimal(agenda.Valor);
                    valorTotalTaxa += 0 + Convert.ToDecimal(agenda.Agendamento.ValorTaxaAdicional);

                    valorTotalTerapeuta += 0 + Convert.ToDecimal(agenda.ValorComissaoTerapeuta);
                    valorTotalEmpresa += 0 + Convert.ToDecimal(valorEmpresaNoServico);

                    valorTotalTaxaPagto += 0 + Convert.ToDecimal(agenda.ValorTaxaProRata);
                    valorTotalMoraPagto += 0 + Convert.ToDecimal(agenda.ValorMoraProRata);

                    // TOTALIZADOR GERAL
                    valorTotalGeral += valorTotal;
                    valorTotalTaxaGeral += valorTotalTaxa;
                    valorTotalTerapeutaGeral += valorTotalTerapeuta;
                    valorTotalEmpresaGeral += valorTotalEmpresa;
                    valorTotalTaxaPagtoGeral += valorTotalTaxaPagto;
                    valorTotalMoraPagtoGeral += valorTotalMoraPagto;
                }

                #region PulaUmaLinhaNaTabela
                relatorio.AppendLine("<TR>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                //relatorio.AppendLine("  <TD>&nbsp;</TD>");
                //relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("</TR>");
                #endregion

                relatorio.AppendLine("<TR>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD>&nbsp;</TD>");
                relatorio.AppendLine("  <TD style='text-align:right;'><b>" + valorTotal.ToString() + "</b></TD>");
                relatorio.AppendLine("  <TD style='text-align:right;'><b>" + valorTotalTaxa.ToString() + "</b></TD>");
                relatorio.AppendLine("  <TD style='text-align:right;'><b>" + valorTotalEmpresa.ToString() + "</b></TH>");
                relatorio.AppendLine("  <TD style='text-align:right;'><b>" + valorTotalTerapeuta.ToString() + "</b></TH>");
                relatorio.AppendLine("  <TD style='text-align:right;'><b>" + valorTotalTaxaPagto.ToString() + "</b></TH>");
                relatorio.AppendLine("  <TD style='text-align:right;'><b>" + valorTotalMoraPagto.ToString() + "</b></TH>");


                relatorio.AppendLine("</TR>");

                relatorio.AppendLine("</tbody></TABLE>");

                relatorio.AppendLine("<BR /><hr /><BR />");
                /* termina aqui */
            }


            relatorio.AppendLine("<H3>RESUMO DO PERÍODO</H3>");

            relatorio.AppendLine(string.Format("<H4>De: {0} - Até: {1}</H4>", DataInicial.ToString("dd/MM/yyyy"), DataFinal.ToString("dd/MM/yyyy")));

            relatorio.AppendLine("<TABLE id='tabela-resumo-periodo' class='table' width='100%'>");
            relatorio.AppendLine("<thead><TR>");
            relatorio.AppendLine("  <TH><b>&nbsp;</b></TH>");
            relatorio.AppendLine("  <TH><b>&nbsp;</b></TH>");
            relatorio.AppendLine("  <TH><b>&nbsp;</b></TH>");
            relatorio.AppendLine("  <TH><b>&nbsp;</b></TH>");
            relatorio.AppendLine("  <TH><b>&nbsp;</b></TH>");
            relatorio.AppendLine("  <TH><b>&nbsp;</b></TH>");
            relatorio.AppendLine("  <TH><b>&nbsp;</b></TH>");
            relatorio.AppendLine("  <TH style='text-align:right;'><b>BRUTO</b></TH>");
            relatorio.AppendLine("  <TH style='text-align:right;'><b>TAXA ADIC.</b></TH>");
            relatorio.AppendLine("  <TH style='text-align:right;'><b>EMPRESA</b></TH>");
            relatorio.AppendLine("  <TH style='text-align:right;'><b>TERAPEUTAS</b></TH>");
            relatorio.AppendLine("  <TH style='text-align:right;'><b>TAXA PAGTO</b></TH>");
            relatorio.AppendLine("  <TH style='text-align:right;'><b>MORA PAGTO</b></TH>");
            relatorio.AppendLine("</TR></thead>");
            relatorio.AppendLine("<tbody><TR>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD style='text-align:right;'>" + valorTotalGeral.ToString() + "</TD>");
            relatorio.AppendLine("  <TD style='text-align:right;'>" + valorTotalTaxaGeral.ToString() + "</TD>");
            relatorio.AppendLine("  <TD style='text-align:right;'>" + valorTotalEmpresaGeral.ToString() + "</TD>");
            relatorio.AppendLine("  <TD style='text-align:right;'>" + valorTotalTerapeutaGeral.ToString() + "</TD>");
            relatorio.AppendLine("  <TD style='text-align:right;'>" + valorTotalTaxaPagtoGeral.ToString() + "</TD>");
            relatorio.AppendLine("  <TD style='text-align:right;'>" + valorTotalMoraPagtoGeral.ToString() + "</TD>");
            relatorio.AppendLine("</TR>");
            relatorio.AppendLine("</tbody></TABLE>");

            ViewBag.relatorio = relatorio.ToString();

            return PartialView();
        }

        [Authorize]
        public async Task<ActionResult> MassagensPorTerapeutas()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			DateTime Inicio = DateTime.Now.AddDays(-30);

            DateTime Fim = DateTime.Now.AddHours(1);

            StringBuilder relatorio = new StringBuilder();

            Decimal valorTotalGeral = 0;
            Decimal valorTotalTaxaGeral = 0;

            // Caches
            var cacheFiliais = await db.FilialModels.AsNoTracking().ToListAsync();
            var cacheServicos = await db.ServicoModels.AsNoTracking().ToListAsync();
            //var cacheStatuses = await db.StatusAgendamentoModels.AsNoTracking().ToListAsync();
            var cachePagtos = await db.TipoPagamentoModels.AsNoTracking().ToListAsync();
            var cacheTerapeutas = await db.TerapeutaModels.AsNoTracking().ToListAsync();



            List<int> terapeutas = new List<int>();

            // Carrega os agendamentos do período
            var agendamentosDoPeriodo = db.AgendamentoModels.Where(x => x.DataInicial >= Inicio && x.DataInicial <= Fim);
            foreach (var item in agendamentosDoPeriodo)
            {
                item.Filial = cacheFiliais.Where(x => x.CodigoFilial == item.CodigoFilial).FirstOrDefault();
                item.TipoPagamento = cachePagtos.Where(x => x.CodigoTipoPagamento == item.CodigoTipoPagamento).FirstOrDefault();
            }

            // Carrega as terapeutas uma a uma
            foreach (var item in terapeutas)
            {

            }


            relatorio.AppendLine("<H3>RESUMO DO PERÍODO</H3>");
            relatorio.AppendLine("<TABLE id='relatorio' class='table' width='100%'>");
            relatorio.AppendLine("<thead><TR>");
            relatorio.AppendLine("  <TH>&nbsp;</TH>");
            relatorio.AppendLine("  <TH>&nbsp;</TH>");
            relatorio.AppendLine("  <TH>&nbsp;</TH>");
            relatorio.AppendLine("  <TH>&nbsp;</TH>");
            relatorio.AppendLine("  <TH>&nbsp;</TH>");
            relatorio.AppendLine("  <TH>&nbsp;</TH>");
            relatorio.AppendLine("  <TH>&nbsp;</TH>");
            relatorio.AppendLine("  <TH>TOTAL</TH>");
            relatorio.AppendLine("  <TH>TOTAL TAXA</TH>");
            relatorio.AppendLine("</TR></thead>");
            relatorio.AppendLine("<tbody><TR>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD>&nbsp;</TD>");
            relatorio.AppendLine("  <TD><b>" + valorTotalGeral.ToString() + "</b></TD>");
            relatorio.AppendLine("  <TD><b>" + valorTotalTaxaGeral.ToString() + "</b></TD>");
            relatorio.AppendLine("</TR>");

            relatorio.AppendLine("</tbody></TABLE>");

            ViewBag.conteudo = relatorio.ToString();

            return View();
        }

        [Authorize]
        public ActionResult RelatorioAgendamentos()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			return View("Agendamentos");
        }

        public ActionResult Agendamentos()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			var agendamentos = GetAgendamentos();

            var lista = from a in agendamentos
                        select new
                        {
                            Filial = a.Filial.Nome,
                            DataInicial = a.DataInicial.ToString(),
                            DataFinal = a.DataFinal.ToString(),
                            Valor = a.Valor.ToString()
                        };

            return Json(lista);
        }

        private List<Agendamento> GetAgendamentos()
        {
            var agendamentos = db.AgendamentoModels
                                 .Include(t => t.Filial)
                                 .Include(t => t.ItensDoAgendamento).ToList();

            return agendamentos;
        }

        public JsonResult ExportarAgendamentos()
        {
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
				  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			Response.ClearContent();

            try
            {
                Response.AddHeader("content-disposition",
                    "attachment; filename=Corpus_Spa_Agendamentos_toExcel_" + DateTime.Now.ToString("yyyyMMddHHmmss") +
                    ".xls");
                Response.ContentType = "application/excel";
                var sw = new StringWriter();
                var htw = new HtmlTextWriter(sw);
                var grid = new GridView();

                var list = db.AgendamentoModels.Include(t => t.Filial)
                                               .Include(t => t.TipoPagamento)
                                               .Include(t => t.ItensDoAgendamento)
                                               .ToList();

                var listJsonRelatorio = from a in list
                                        select new
                                        {
                                            CodigoAgendamento = a.CodigoAgendamento,
                                            Filial = a.Filial.Nome.ToString(),
                                            DataInicial = a.DataInicial,
                                            DataFinal = a.DataFinal,
                                            Valor = a.Valor,
                                            ValorEmpresa = a.ValorEmpresa,
                                            ValorTerapeuta = a.ValorTerapeuta,
                                            ValorLiquido = a.ValorLiquido,
                                            ValorMoraFormaPagto = a.ValorMoraFormaPagto,
                                            ValorTaxaFormaPagto = a.ValorTaxaFormaPagto,
                                            ValorTaxaAdicional = a.ValorTaxaAdicional,
                                            Itens = from i in a.ItensDoAgendamento
                                                    select new { CodigoServico = i.CodigoServico,
                                                                 CodigoTerapeuta = i.CodigoTerapeuta,
                                                                 QtdePeriodos = i.QtdePeriodos,
                                                                 TipoDeMassagem = i.TipoDeMassagem,
                                                                 Valor = i.Valor,
                                                                 ValorEmpresa = i.ValorComissaoEmpresa,
                                                                 ValorTerapeuta = i.ValorComissaoTerapeuta,
                                                                 ValorTaxaProRata = i.ValorTaxaProRata,
                                                                 ValorMoraProRata = i.ValorMoraProRata
                                                        
                                                    },
                                            IPdoCliente = a.IPdoCliente,
                                            NomeCliente = a.NomeCliente,
                                            EmailDoCliente = a.EmailDoCliente,
                                            Observacao = a.Observacao,
                                            StatusDoAgendamento = a.CodigoStatusAgendamento,
                                            FormaPagto = a.CodigoTipoPagamento                                                                                        
                                        };

                grid.DataSource = listJsonRelatorio;
                grid.DataBind();
                grid.RenderControl(htw);
                Response.Write(sw.ToString());
                Response.End();                
            }
            catch (Exception ex)
            {
                TempData["erro"] = "Erro: " + ex.Message;                
            }

            return Json(Response);

        }


    }
}