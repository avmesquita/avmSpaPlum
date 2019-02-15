using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Models
{
    public class FiltroAgendamentoViewModel
    {
        public int CodigoFilial { get; set; }
        public String DataInicial { get; set; }
        public String DataFinal { get; set; }
        public int? CodigoTerapeuta { get; set; }
        public int? CodigoServico { get; set; }
        public int? CodigoStatusAgendamento { get; set; }
        public int? CodigoTipoPagamento { get; set; }
        public IList<Agendamento> Resultado { get; set; }


    }
}