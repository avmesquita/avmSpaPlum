using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPlum.Web.Entity
{
    [Serializable]
    [Table("TB_PROMOCAO_CLIENTE")]
    public class PromocaoCliente
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_PROMOCAO_CLIENTE", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoPromocaoCliente { get; set; }

        [ForeignKey("Promocao")]
        [Display(Name = "Promoção")]
        [Column("COD_PROMOCAO", Order = 2)]
        public int CodigoPromocao { get; set; }

        public virtual Promocao Promocao { get; set; }

        [Display(Name = "Motivação")]
        [Column("TXT_NOME", Order = 3)]
        [Required(ErrorMessage = "Informe uma descrição para a motivação da promoção")]
        public string Motivacao { get; set; }

        [Display(Name = "E-Mail do Beneficiário")]
        [Column("TXT_EMAIL", Order = 4)]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Informe uma descrição para a promoção")]
        public string ContaBeneficiaria { get; set; }

        [Display(Name ="Quantidade Máxima de Utilização")]
        [Column("NUM_QTDE_MAXIMA", Order = 5)]        
        public int? QuantidadeMaximaUtilizacoes { get; set; }

        [Display(Name = "Data da Última Utilização")]
        [Column("DAT_ULTIMA_UTILIZACAO", Order = 6)]
        [DataType(DataType.DateTime)]        
        public DateTime? DataUltimaUtilizacao { get; set; }

        public PromocaoCliente()
        {
            this.CodigoPromocao = 0;
            this.CodigoPromocaoCliente = 0;
            this.ContaBeneficiaria = "";
            this.DataUltimaUtilizacao = DateTime.Now;
            this.Motivacao = "";
            this.QuantidadeMaximaUtilizacoes = 0;            
        }
    }
}