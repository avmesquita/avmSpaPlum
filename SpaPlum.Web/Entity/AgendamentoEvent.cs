using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Data;
using System.Data.Entity;
using System.Data.Common;
using SpaPlum.Web.Contexto;
using SpaPlum.Web.Service;

namespace SpaPlum.Web.Entity
{
    public class AgendamentoEvent
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        public int ID;
        public string Title;
        public int SomeImportantKeyID;
        public string StartDateString;
        public string EndDateString;
        public string StatusString;
        public string StatusColor;
        public string ClassName;

        private int DiferencaEmMinutos(DateTime Inicial, DateTime Final)
        {
            TimeSpan span = Final.Subtract(Inicial);

            return span.Minutes;
        }


        public List<AgendamentoEvent> GetAllAgendamentosInDateRange(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);

            var terapeutas = db.TerapeutaModels.ToList();
            var servicos = db.ServicoModels.ToList();
            //var statusAgendamentos = db.StatusAgendamentoModels.ToList();

            var rslt = db.AgendamentoModels.Where(s => s.DataInicial >= fromDate && s.DataFinal <= toDate)
                                           .Include(t => t.Filial)
                                           .Include(t => t.TipoPagamento)
                                           .Include(t => t.ItensDoAgendamento);
                                           

            List<AgendamentoEvent> result = new List<AgendamentoEvent>();

            foreach (var item in rslt)
            {
                AgendamentoEvent rec = new AgendamentoEvent();
                rec.ID = item.CodigoAgendamento;
                rec.SomeImportantKeyID = item.CodigoStatusAgendamento;
                rec.StartDateString = item.DataInicial.ToString("s"); // "s" is a preset format that outputs as: "2009-02-27T12:12:22"
                rec.EndDateString = Convert.ToDateTime(item.DataFinal).ToString("s"); // field AppointmentLength is in minutes
                string Titulo = "";

                Titulo += "CLIENTE: [" + item.NomeCliente.ToString() + "]\n\n";

                foreach (var itemAgendamento in item.ItensDoAgendamento)
                {
                    Terapeuta terapeuta = terapeutas.Where(x => x.CodigoTerapeuta == itemAgendamento.CodigoTerapeuta).FirstOrDefault();
                    Servico servico = servicos.Where(x => x.CodigoServico == itemAgendamento.CodigoServico).FirstOrDefault();                 

                    string tipoMassagem = "";
                    switch (itemAgendamento.TipoDeMassagem)
                    {
                        case 0: tipoMassagem = "Padrão"; break;
                        case 1: tipoMassagem = "4 Mãos"; break;
                        case 2: tipoMassagem = "Casais"; break;
                    }
                                        
                    Titulo += "SERVICO  : [" + servico.Nome.ToString() + "(" + tipoMassagem + ")]\n";
                    Titulo += "TERAPEUTA: [" + terapeuta.Nome.ToString() + "]\n";
                    Titulo += "PERIODOS : [" + itemAgendamento.QtdePeriodos.ToString() + "]\n\n";
                }

                string status = @Enum.GetName(typeof(SpaPlum.Web.Entity.StatusAgendamento), item.CodigoStatusAgendamento);
                Titulo += "STATUS: [" + status + "]\n";

                rec.Title = Titulo;

                rec.StatusString = Enums.GetName<AppointmentStatus>((AppointmentStatus)item.CodigoStatusAgendamento);
                rec.StatusColor = Enums.GetEnumDescription<AppointmentStatus>(rec.StatusString);

                string ColorCode = rec.StatusColor.Substring(0, rec.StatusColor.IndexOf(":"));
                rec.ClassName = rec.StatusColor.Substring(rec.StatusColor.IndexOf(":") + 1, rec.StatusColor.Length - ColorCode.Length - 1);
                rec.StatusColor = ColorCode;
                result.Add(rec);


            }

            return result;
        }


        public List<AgendamentoEvent> LoadAppointmentSummaryInDateRange(double start, double end)
        {
            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);

            var rslt = db.AgendamentoModels.Include(t => t.Filial)
                                           .Include(t => t.TipoPagamento)
                                           .Include(t => t.ItensDoAgendamento)
                                           .Where(s => s.DataInicial >= fromDate && s.DataFinal <= toDate)
                                                    .GroupBy(s => System.Data.Entity.DbFunctions.TruncateTime(s.DataInicial))
                                                    .Select(x => new { DateTimeScheduled = x.Key, Count = x.Count() });

            List<AgendamentoEvent> result = new List<AgendamentoEvent>();
            int i = 0;
            foreach (var item in rslt)
            {
                AgendamentoEvent rec = new AgendamentoEvent();
                rec.ID = i; //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                rec.SomeImportantKeyID = -1;
                string StringDate = string.Format("{0:yyyy-MM-dd}", item.DateTimeScheduled);
                rec.StartDateString = StringDate + "T00:00:00"; //ISO 8601 format
                rec.EndDateString = StringDate + "T23:59:59";
                rec.Title = "Confirmados: " + item.Count.ToString();
                result.Add(rec);
                i++;
            }

            return result;
        }

        public List<AgendamentoEvent> LoadAppointmentDetailInDateRange(double start, double end)
        {
            var terapeutas = db.TerapeutaModels.ToList();
            var servicos = db.ServicoModels.ToList();

            var fromDate = ConvertFromUnixTimestamp(start);
            var toDate = ConvertFromUnixTimestamp(end);

            var rslt = db.AgendamentoModels.Include(t => t.Filial)
                                           .Include(t => t.TipoPagamento)
                                           .Include(t => t.ItensDoAgendamento)
                                           .Where(s => s.DataInicial >= fromDate && s.DataFinal <= toDate);
                                           //.Include(t => t.Terapeuta)
                                           //.Include(t => t.Servico);

            List<AgendamentoEvent> result = new List<AgendamentoEvent>();
            //int i = 0;
            foreach (var item in rslt)
            {
                AgendamentoEvent rec = new AgendamentoEvent();
                rec.ID = item.CodigoAgendamento; //we dont link this back to anything as its a group summary but the fullcalendar needs unique IDs for each event item (unless its a repeating event)
                rec.SomeImportantKeyID = item.CodigoStatusAgendamento;
                string dataInicial = string.Format("{0:yyyy-MM-dd HH:mm}", item.DataInicial);
                string dataFinal = string.Format("{0:yyyy-MM-dd HH:mm}", item.DataFinal);
                string horaFinal = string.Format("{0:HH:mm}", item.DataFinal);
                rec.StartDateString = dataInicial; // + "T00:00:00"; //ISO 8601 format
                rec.EndDateString = dataFinal; // + "T23:59:59";

                string Titulo = "";

                Titulo += "CLIENTE: [" + item.NomeCliente.ToString() + "]\n\n";

                foreach (var itemAgendamento in item.ItensDoAgendamento)
                {
                    Terapeuta terapeuta = terapeutas.Where(x => x.CodigoTerapeuta == itemAgendamento.CodigoTerapeuta).FirstOrDefault();
                    Servico servico = servicos.Where(x => x.CodigoServico == itemAgendamento.CodigoServico).FirstOrDefault();

                    string tipoMassagem = "";
                    switch (itemAgendamento.TipoDeMassagem)
                    {
                        case 0: tipoMassagem = "Padrão"; break;
                        case 1: tipoMassagem = "4 Mãos"; break;
                        case 2: tipoMassagem = "Casais"; break;
                    }

                    Titulo += "SERVICO  : [" + servico.Nome.ToString() + "(" + tipoMassagem + ")]\n";
                    Titulo += "TERAPEUTA: [" + terapeuta.Nome.ToString() + "]\n";
                    Titulo += "PERIODOS : [" + itemAgendamento.QtdePeriodos.ToString() + "]\n\n";
                }

                string status = @Enum.GetName(typeof(SpaPlum.Web.Entity.StatusAgendamento), item.CodigoStatusAgendamento);
                Titulo += "STATUS: [" + status + "]\n";

                rec.Title = Titulo;

                result.Add(rec);
            }

            return result;
        }




        public void UpdateDiaryEvent(int id, string NewEventStart, string NewEventEnd)
        {
            var rec = db.AgendamentoModels.FirstOrDefault(s => s.CodigoAgendamento == id);
            if (rec != null)
            {
                if (!String.IsNullOrEmpty(NewEventEnd))
                {
                    //TimeSpan span = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime() - DateTimeStart;

                    DateTime DateTimeStart = DateTime.Parse(NewEventStart, null, DateTimeStyles.RoundtripKind).ToLocalTime(); // and convert offset to localtime
                    DateTime DateTimeEnd = DateTime.Parse(NewEventEnd, null, DateTimeStyles.RoundtripKind).ToLocalTime();

                    rec.DataInicial = DateTimeStart;
                    rec.DataFinal = DateTimeEnd;                    

                    db.Entry(rec).State = EntityState.Modified;
                    db.SaveChangesAsync();
                }
            }
        }


        private static DateTime ConvertFromUnixTimestamp(double timestamp)
        {
            var origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return origin.AddSeconds(timestamp);
        }


        public bool CreateNewEvent(int codigoFilial, int codigoServico, int codigoStatusAgendamento, int codigoTerapeuta,
                                   string nomeCliente, string emailCliente, string observacao, int qtdePeriodos, int tipoMassagem, 
                                   decimal valor, decimal valorEmpresa, decimal valorTerapeuta, decimal valorLiquido, decimal valorTaxaAdicional,
                                   decimal valorTaxaPagto, decimal valorMoraPagto, string DataInicio, string DataFim)
        {
            try
            {
                Agendamento agendamento = new Agendamento();

                agendamento.CodigoFilial = codigoFilial;
                //agendamento.CodigoServico = codigoServico;
                agendamento.CodigoStatusAgendamento = codigoStatusAgendamento;
                //agendamento.CodigoTerapeuta = codigoTerapeuta;
                agendamento.DataInicial = DateTime.ParseExact(DataInicio, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                agendamento.DataFinal = DateTime.ParseExact(DataFim, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                agendamento.NomeCliente = nomeCliente;
                agendamento.EmailDoCliente = emailCliente;
                agendamento.Observacao = observacao;
                //agendamento.QuantidadePeriodos = qtdePeriodos;
                //agendamento.TipoDeMassagem = tipoMassagem;
                agendamento.Valor = valor;
                agendamento.ValorEmpresa = valorEmpresa;
                agendamento.ValorTerapeuta = valorTerapeuta;
                agendamento.ValorLiquido = valorLiquido;
                agendamento.ValorTaxaAdicional = valorTaxaAdicional;
                agendamento.ValorTaxaFormaPagto = valorTaxaPagto;
                agendamento.ValorMoraFormaPagto = valorMoraPagto;

                agendamento.Filial = db.FilialModels.Find(agendamento.CodigoFilial);
                //agendamento.Servico = db.ServicoModels.Find(agendamento.CodigoServico);
                //agendamento.Servico.Filial = agendamento.Filial;                
                //agendamento.Terapeuta = db.TerapeutaModels.Find(agendamento.CodigoTerapeuta);
                //agendamento.Terapeuta.Filial = agendamento.Filial;
                agendamento.TipoPagamento = db.TipoPagamentoModels.Find(agendamento.CodigoTipoPagamento);

                AgendamentoItemServico item = new AgendamentoItemServico();
                item.CodigoAgendamento = agendamento.CodigoAgendamento;
                item.Agendamento = agendamento;
                //item.CodigoServico = agendamento.CodigoServico;
                //item.Valor = agendamento.Servico.Valor;
                //item.ValorComissaoEmpresa = (!agendamento.Servico.EmPromocao) ? agendamento.Servico.ValorComissaoEmpresa : agendamento.Servico.ValorPromocaoComissaoEmpresa;
                //item.ValorComissaoTerapeuta = (!agendamento.Servico.EmPromocao) ? agendamento.Servico.ValorComissaoTerapeuta : agendamento.Servico.ValorPromocaoComissaoTerapeuta;

                agendamento.ItensDoAgendamento.Add(item);

                db.AgendamentoModels.Add(agendamento);
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}