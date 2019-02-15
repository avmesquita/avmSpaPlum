using SpaPlum.Web.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Models
{
    public class AgendamentoModel
    {
        public int CodigoAgendamento { get; set; }
        public string NomeCliente { get; set; }
        public string EmailDoCliente { get; set; }
        public string TelefoneDoCliente { get; set; }
        public string ClubeVIP { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public decimal? Valor { get; set; }
        public string Observacao { get; set; }
        public int CodigoFilial { get; set; }
        public int CodigoStatusAgendamento { get; set; }
        public int CodigoTipoPagamento { get; set; }
        public string IPdoCliente { get; set; }
        public string SessionID { get; set; }
        public decimal ValorTaxaAdicional { get; set; }
        public virtual ICollection<AgendamentoItemServico> ItensDoAgendamento { get; set; }
        public string Senha { get; set; }
        public string ConfirmarSenha { get; set; }


    }
}