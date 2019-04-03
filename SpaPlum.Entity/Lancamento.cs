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
    [Table("TB_LANCAMENTO")]
    public class Lancamento
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_LANCAMENTO")]
        public int CodigoLancamento { get; set; }

        [Display(Name = "Título")]
        [Column("TXT_TITULO")]
        [Required(ErrorMessage = "Informe o título do documento")]
        public string Titulo { get; set; }

        [Display(Name = "Descrição")]
        [Column("TXT_DESCRICAO")]
        [DataType(DataType.MultilineText)]
        public string Descricao { get; set; }

		[Display(Name = "Data do Vencimento")]
		[Column("DAT_VENCIMENTO")]
		[DataType(DataType.Date)]
		public DateTime? DataVencimento { get; set; }

		[Display(Name = "Data do Pagamento")]
        [Column("DAT_PAGAMENTO")]
        [DataType(DataType.Date)]
        public DateTime? DataPagamento { get; set; }

        [Display(Name = "Data do Lancamento")]
        [Column("DAT_LANCAMENTO")]
        [DataType(DataType.DateTime)]
        public DateTime DataLancamento { get; set; }

        //[Display(Name = "Tipo de Lancamento")]
        //[Column("TIP_LANCAMENTO")]
        //[Required(ErrorMessage = "Informe o tipo de lançamento")]
        //public string TipoLancamento { get; set; }

        [Display(Name = "Valor")]
        [Column("VAL_VALOR")]
        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Informe o valor da conta")]
        public decimal Valor { get; set; }

		[Display(Name = "Multa")]
		[Column("VAL_VALOR_MULTA")]
		[DataType(DataType.Currency)]
		public decimal? ValorMulta { get; set; } = 0;

		// -- Colunas de FKs

		[Display(Name = "Forma de Pagamento")]
        [Column("COD_TIPO_PAGAMENTO")]
        [Required(ErrorMessage = "Informe a forma de pagamento")]
        public int CodigoTipoPagamento { get; set; }

        [Display(Name = "Tipo de Operacao")]
        [Column("COD_TIPO_OPERACAO")]
        [Required(ErrorMessage = "Informe o tipo de operação")]
        public int CodigoTipoOperacao { get; set; }

        [Display(Name = "Filial")]
        [Column("COD_FILIAL")]
        public int CodigoFilial { get; set; }

		[Display(Name = "Agendamento")]
		[Column("COD_AGENDAMENTO")]		
		public int? CodigoAgendamento { get; set; }

		// -- FKs

		[ForeignKey("CodigoTipoPagamento")]
        public virtual TipoPagamento TipoPagamento { get; set; }

        //[ForeignKey("CodigoTipoOperacao")]
        //public virtual TipoOperacao TipoOperacao { get; set; }

        [ForeignKey("CodigoFilial")]
        public virtual Filial Filial { get; set; }

		[ForeignKey("CodigoAgendamento")]
		public virtual Agendamento Agendamento { get; set; }


		public Lancamento()
        {
            this.CodigoFilial = 0;
            this.CodigoLancamento = 0;
            this.CodigoTipoOperacao = 0;
            this.CodigoTipoPagamento = 0;
			this.CodigoAgendamento = null;
            this.DataLancamento = DateTime.Now;
            this.DataPagamento = null;
			this.DataVencimento = null;
            this.Descricao = "";
			// this.TipoLancamento = "";
            this.Titulo = "";
            this.Valor = 0;
			this.ValorMulta = 0;
        }


    }
}
