using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPlum.Entity
{
    [Serializable]
    [Table("TB_TIPO_PAGAMENTO")]
    public class TipoPagamento
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_TIPO_PAGAMENTO")]
        public int CodigoTipoPagamento { get; set; }


        [Display(Name ="Filial")]
        [Column("COD_FILIAL")]
        public int CodigoFilial { get; set; }

        [ForeignKey("CodigoFilial")]
        public Filial Filial { get; set; }

        [Display(Name = "Descrição")]
        [Column("TXT_DESCRICAO")]
        [Required(ErrorMessage = "Informe o nome da forma de pagamento")]
        public string Descricao { get; set; }

        [Display(Name = "Taxa")]
        [Column("VAL_TAXA")]        
        [Required(ErrorMessage = "Informe o valor da taxa")]
        public decimal Taxa { get; set; }

        /// <summary>
        /// /// Indica se o Valor da Taxa é Percentual ou Real
        /// </summary>
        [Display(Name = "Tipo Taxa (R/P)")]
        [Column("TIP_TAXA")]        
        public string TipoTaxa { get; set; }

        [Display(Name = "Mora")]
        [Column("VAL_MORA")]        
        [Required(ErrorMessage = "Informe o valor da mora")]
        public decimal Mora { get; set; }

        /// <summary>
        /// Indica se o Valor de Mora é Percentual ou Real
        /// </summary>
        [Display(Name = "Tipo Mora (R/P)")]
        [Column("TIP_MORA")]
        public string TipoMora { get; set; }

        [Display(Name = "Ativo")]
        [Column("IND_ATIVO")]
        public bool Ativo { get; set; }

        [Display(Name ="Gateway")]
        [Column("IND_GATEWAY")]
        public bool GatewayAtivo { get; set; }


        [Display(Name ="Gateway de Pagamento")]
        [Column("COD_GATEWAY_PAGAMENTO")]
        public int CodigoGatewayPagamento { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; }
        public virtual ICollection<Lancamento> Lancamentos { get; set; }

        public TipoPagamento()
        {
            this.CodigoTipoPagamento = 0;
            this.Descricao = "";
            this.TipoMora = "R";
            this.Mora = 0;
            this.TipoTaxa = "R";
            this.Taxa = 0;
            this.Ativo = true;
            this.GatewayAtivo = false;
            this.CodigoGatewayPagamento = 0;
            this.CodigoFilial = 0;

            this.Agendamentos = new List<Agendamento>();
            this.Lancamentos = new List<Lancamento>();
        }
    }
}