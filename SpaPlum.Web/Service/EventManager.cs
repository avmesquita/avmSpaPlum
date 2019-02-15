using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using System.Linq;
using System.Data.Entity;

namespace SpaPlum.Web.Service
{
    public class EventManager
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        /*
        public EventManager(Controller controller, string key)
        {
            this.controller = controller;
            this.key = key;

            if (this.controller.Session[key] == null)
            {
                this.controller.Session[key] = generateData();
            }
        }
     

        public DataTable Data
        {
            get { return (DataTable)controller.Session[key]; }
        }
        */

        public DataTable FilteredData(DateTime start, DateTime end)
        {
            //string where = String.Format("NOT (([end] <= '{0:s}') OR ([start] >= '{1:s}'))", start, end);

            var agendamentos = db.AgendamentoModels.Where(x => x.DataInicial > start && x.DataInicial < end);
            var lista = agendamentos.ToList();
            var dt = new Tipos().ToDataTable<Agendamento>(lista);

            //DataRow[] rows = Data.Select(where)
            //DataTable filtered = Data.Clone();

            //foreach (DataRow r in rows)
            //{
            //    filtered.ImportRow(r);
            //}

            return dt;
        }


        public void EventEdit(string id, string name)
        {
            var agendamento = db.AgendamentoModels.Find(Int32.Parse(id));

            // AQUI FAZ AS ALTERACOES

            //db.AgendamentoModels.Add(agendamento);
            
            //db.SaveChanges();

        }

        public void EventMove(string id, DateTime start, DateTime end)
        {
            var agendamento = db.AgendamentoModels.Find(Int32.Parse(id));

            if (agendamento != null)
            {
                agendamento.DataInicial = start;
                agendamento.DataFinal = end;

                db.Entry(agendamento).State = EntityState.Modified;                
                db.SaveChanges();
            }
        }

        public Event Get(string id)
        {
            var agendamento = db.AgendamentoModels.Find(Int32.Parse(id));
            string tipoMassagem = "";
            /*
            switch (agendamento.TipoDeMassagem)
            {
                case 0: tipoMassagem = "Massagem Padrão";
                    break;
                case 1:
                    tipoMassagem = "Massagem 4 Mãos";
                    break;
                case 2:
                    tipoMassagem = "Massagem Para Casais";
                    break;
            }
            */
            if (agendamento != null)
            {
                string texto = "Filial        = " + db.FilialModels.Find(agendamento.CodigoFilial).Nome +
                               //"Terapia       = " + db.ServicoModels.Find(agendamento.CodigoServico).Nome +
                               //"Terapeuta     = " + db.TerapeutaModels.Find(agendamento.CodigoTerapeuta).Nome +
                               "Tipo Massagem = " + tipoMassagem +
                               "Valor         = " + agendamento.Valor.ToString() +
                               "Forma Pagto   = " + db.TipoPagamentoModels.Find(agendamento.CodigoTipoPagamento).Descricao;
                  
                return new Event
                {
                    Id = agendamento.CodigoAgendamento.ToString(),
                    Text = texto,
                    Start = agendamento.DataInicial,
                    End = Convert.ToDateTime(agendamento.DataFinal)
                };
            }
            return null;
        }

        internal void EventCreate(DateTime start, DateTime end, string text)
        {
            /*
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["daypilot"].ConnectionString))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand("INSERT INTO [event] (eventstart, eventend, name) VALUES (@start, @end, @name); ", con);  // SELECT SCOPE_IDENTITY();
                cmd.Parameters.AddWithValue("start", start);
                cmd.Parameters.AddWithValue("end", end);
                cmd.Parameters.AddWithValue("name", text);
                cmd.ExecuteScalar();
            }
            */
        }

        public class Event
        {
            public string Id { get; set; }
            public string Text { get; set; }
            public DateTime Start { get; set; }
            public DateTime End { get; set; }
        }

        public void EventDelete(string id)
        {
            var agendamento = db.AgendamentoModels.Find(id);

            if (agendamento != null)
            {
                db.AgendamentoModels.Remove(agendamento);
                db.SaveChanges();
            }
        }
    }
}
