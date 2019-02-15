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
    [Table("TB_PROMOCAO")]
    public class Promocao
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_PROMOCAO", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoPromocao { get; set; }

        [ForeignKey("Filial")]
        [Display(Name = "Filial")]
        [Column("COD_FILIAL", Order = 2)]
        public int CodigoFilial { get; set; }

        public virtual Filial Filial { get; set; }

        [Display(Name = "Descrição")]
        [Column("TXT_NOME", Order = 3)]
        [Required(ErrorMessage = "Informe uma descrição para a promoção")]
        public string Nome { get; set; }

        [Display(Name = "Descrição")]
        [Column("TXT_NOME_HTML", Order = 4)]
        [Required(ErrorMessage = "Informe a descrição que aparecerá no site")]
        public string NomeHtml { get; set; }

        [Display(Name = "Tipo de Promoção")]
        [Column("TIP_PROMOCAO", Order = 5)]        
        [Required(ErrorMessage = "Informe tipo de promoção")]
        public int TipoPromocao { get; set; }

        [Display(Name = "Valor")]
        [Column("VAL_VALOR", Order = 6)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe o valor do serviço")]
        public decimal Valor { get; set; }

        [Display(Name = "Token")]
        [Column("TXT_TOKEN", Order = 7)]
        [Required(ErrorMessage = "Favor informar o Token")]
        public string Token { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Column("DAT_CADASTRO", Order = 8)]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Favor informar o Token")]
        public DateTime DataCadastro { get; set; }

        [Display(Name = "Data de Início de Vigência")]
        [Column("DAT_INICIO_VIGENCIA", Order = 9)]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Favor a data de início de vigência da promoção")]
        public DateTime DataInicioVigencia { get; set; }

        [Display(Name = "Data de Fim de Vigência")]
        [Column("DAT_FIM_VIGENCIA", Order = 10)]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Favor a data de fim de vigência da promoção")]
        public DateTime DataFimVigencia { get; set; }

        [Display(Name = "Tempo Mínimo (Minutos)")]
        [Column("NUM_MINUTOS_MINIMO", Order = 11)]
        [Required(ErrorMessage = "Informe o tempo mínimo para dar desconto (em minutos)")]
        public int TempoMinimo { get; set; }

        [Display(Name = "Valor Mínimo")]
        [Column("VAL_VALOR_MINIMO", Order = 12)]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe o valor mínimo para dar desconto")]
        public decimal ValorMinimo { get; set; }

        [Display(Name ="Ativo")]
        [Column("IND_ATIVO", Order = 13)]
        public bool Ativo { get; set; }

        public virtual ICollection<PromocaoCliente> PromocaoCliente { get; set; }

        public Promocao()
        {
            this.Ativo = true;
            this.CodigoFilial = 0;
            this.CodigoPromocao = 0;
            this.DataCadastro = DateTime.Now;
            this.DataFimVigencia = DateTime.Now;
            this.DataInicioVigencia = DateTime.Now;
            this.Nome = "";
            this.NomeHtml = "";
            this.TempoMinimo = 0;
            this.TipoPromocao = -1;
            this.Token = "";
            this.Valor = 0;
            this.ValorMinimo = 0;

            PromocaoCliente = new List<PromocaoCliente>();
        }
    }
}