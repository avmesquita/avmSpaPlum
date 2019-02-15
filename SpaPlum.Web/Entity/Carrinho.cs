using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Entity
{
    [Serializable]
    [Table("TB_CARRINHO")]
    public class Carrinho
    {
        [Key]
        [Column("COD_CARRINHO",Order = 1)]
        public Int64 CodigoCarrinho { get; set; }

        [Column("TXT_SESSION_ID", Order = 2)]
        public string SessionID { get; set; }

        [Column("TXT_IP_CLIENTE", Order = 3)]
        public string IPCliente { get; set; }

        [Column("DAT_CARRINHO", Order = 4)]
        public DateTime Data { get; set; }

        // FK VIRTUAL :: SO TERÁ FK SE HOUVER PERSISTENCIA DO CARRINHO
        [Column("COD_AGENDAMENTO", Order = 5)]
        public Int64? CodigoAgendamento { get; set; }

        public virtual IList<CarrinhoItem> Servicos { get; set; }

        public Carrinho()
        {
            this.SessionID = "";
            this.IPCliente = "127.0.0.1";
            this.Data = DateTime.Now;
            this.Servicos = new List<CarrinhoItem>();
            
        }

        public Carrinho(string sessionid, string ipcliente)
        {
            this.SessionID = sessionid;
            this.IPCliente = ipcliente;
            this.Data = DateTime.Now;
            this.Servicos = new List<CarrinhoItem>();
        }

    }
}