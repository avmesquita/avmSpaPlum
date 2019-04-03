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
    [Table("TB_AGENDAMENTO")]
    public class Agendamento
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_AGENDAMENTO")]
        public int CodigoAgendamento { get; set; }

        [Display(Name = "Nome do Cliente")]
        [Column("TXT_NOME")]
        [Required(ErrorMessage = "Informe você quer ser chamado")]
        public string NomeCliente { get; set; }

        [Display(Name = "Email do Cliente")]
        [Column("TXT_EMAIL")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Informe o e-mail, por favor")]
        public string EmailDoCliente { get; set; }


        [Display(Name = "Telefone")]
        [Column("TXT_TELEFONE")]
        [DataType(DataType.PhoneNumber)]        
        public string TelefoneDoCliente { get; set; }


        [Display(Name = "Código VIP")]
        [Column("TXT_CLUBE_VIP")]
        public string ClubeVIP { get; set; }

        [Display(Name = "Data de Início")]
        [Column("DAT_INICIO")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        [Required(ErrorMessage = "Informe quando desejar marcar")]
        public DateTime DataInicial { get; set; }

        [Display(Name = "Data Final")]
        [Column("DAT_FIM")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? DataFinal { get; set; }

        [Display(Name = "Valor")]
        [Column("VAL_VALOR")]
        [DataType(DataType.Currency)]
        //[Required(ErrorMessage = "Informe o preço do serviço")]
        public decimal? Valor { get; set; }

        [Display(Name = "Valor da Taxa Adicional")]
        [Column("VAL_TAXA_ADICIONAL")]
        [DataType(DataType.Currency)]
        public decimal? ValorTaxaAdicional { get; set; }

        [Display(Name = "Taxa da Forma de Pagto")]
        [Column("VAL_TAXA_FORMA_PAGTO")]
        [DataType(DataType.Currency)]
        public decimal? ValorTaxaFormaPagto { get; set; }

        [Display(Name = "Taxa da Forma de Pagto")]
        [Column("VAL_MORA_FORMA_PAGTO")]
        [DataType(DataType.Currency)]
        public decimal? ValorMoraFormaPagto { get; set; }

        [Display(Name = "Valor da Empresa")]
        [Column("VAL_FINAL_EMPRESA")]
        [DataType(DataType.Currency)]
        public decimal? ValorEmpresa { get; set; }

        [Display(Name = "Valor da Empresa")]
        [Column("VAL_FINAL_TERAPEUTA")]
        [DataType(DataType.Currency)]
        public decimal? ValorTerapeuta { get; set; }

        [Display(Name = "Valor Líquido")]
        [Column("VAL_LIQUIDO")]
        [DataType(DataType.Currency)]
        public decimal? ValorLiquido { get; set; }

        /// <summary>
        /// TipoDeMassagem define se é SÓ A MASSAGEM ESCOLHIDA (0) ou se É EM GRUPO. Se GRUPO, pode ser 4 Mãos (1) e Casais(2)
        /// </summary>
        //[Display(Name = "Tipo")]
        //[Column("TIP_MASSAGEM")]
        //public int TipoDeMassagem { get; set; }

        //[Display(Name = "Quantos Periodos")]
        //[Column("NUM_PERIODOS")]
        //[Range(1, 12, ErrorMessage = "Somente são aceitas opções entre 1 e 12 períodos.")]
        //public int QuantidadePeriodos { get; set; }


        [Display(Name = "Observação")]
        [Column("TXT_OBSERVACAO")]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Autenticação")]
        [Column("TXT_AUTENTICACAO")]
        public string Autenticacao { get; set; }

        // -- Colunas de FKs

        [Display(Name = "Filial")]
        [Column("COD_FILIAL")]
        public int CodigoFilial { get; set; }

        [ForeignKey("CodigoFilial")]
        public virtual Filial Filial { get; set; }


        //[Display(Name = "Código do Serviço")]
        //[Column("COD_SERVICO")]
        //[Required(ErrorMessage = "Informe o serviço desejado")]
        //public int CodigoServico { get; set; }

        //[ForeignKey("CodigoServico")]
        //public virtual Servico Servico { get; set; }


        [Display(Name = "Status")]
        [Column("COD_STATUS_AGENDAMENTO")]
        [Required(ErrorMessage = "Informe o status da agenda")]
        public int CodigoStatusAgendamento { get; set; }

        //[ForeignKey("CodigoStatusAgendamento")]
        //public virtual StatusAgendamento Status { get; set; }

        //[Display(Name = "Código da Terapeuta")]
        //[Column("COD_TERAPEUTA")]
        //[Required(ErrorMessage = "Informe o código da Terapeuta")]
        //public int CodigoTerapeuta { get; set; }

        //[ForeignKey("CodigoTerapeuta")]
        //public virtual Terapeuta Terapeuta { get; set; }


        [Display(Name = "´Código do Pagamento")]
        [Column("COD_TIPO_PAGAMENTO")]
        [Required(ErrorMessage = "Informe a forma de pagamento")]
        public int CodigoTipoPagamento { get; set; }

        [ForeignKey("CodigoTipoPagamento")]
        public virtual TipoPagamento TipoPagamento { get; set; }

        [Display(Name = "Endereço IP")]
        [Column("TXT_IP_CLIENTE")]
        public string IPdoCliente { get; set; }

        [Display(Name = "SESSIONID")]
        [Column("TXT_SESSION_ID")]
        public string SessionID { get; set; }

        [Display(Name = "Token Paypal")]
        [Column("TXT_TOKEN_PAYPAL")]
        public string TokenPayPal { get; set; }

		[NotMapped]
		[Display(Name = "Hora Desejada")]
		public DateTime HoraInicial { get; set; }
        
        public virtual ICollection<AgendamentoItemServico> ItensDoAgendamento { get; set; }

        public Agendamento()
        {
            this.CodigoAgendamento = 0;
            this.NomeCliente = "";
            this.EmailDoCliente = "";
            this.ClubeVIP = "";

            if (DateTime.Now > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 21, 0, 0))
            {
                this.DataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 9, 0, 0);
            }
            else
            {
                var dataFutura = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour + 1, 0, 0);
                if (DateTime.Now > dataFutura)
                {
                    this.DataInicial = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1, 9, 0, 0);
                }
                else
                {
                    this.DataInicial = dataFutura;
                }
            }

            this.DataFinal = null;
            this.Valor = 0;
            this.ValorTaxaAdicional = 0;
            //this.CodigoServico = 0;
            this.CodigoStatusAgendamento = 0;
            //this.CodigoTerapeuta = 0;
            this.CodigoTipoPagamento = 0;
            this.CodigoFilial = 0;
            //this.QuantidadePeriodos = 1;
            //this.TipoDeMassagem = 0;
            this.Observacao = "";

            this.ValorTaxaFormaPagto = 0;
            this.ValorMoraFormaPagto = 0;
            this.ValorEmpresa = 0;
            this.ValorTerapeuta = 0;

            this.SessionID = "";
            this.IPdoCliente = "127.0.0.1";
            this.TokenPayPal = "";

            this.ItensDoAgendamento = new List<AgendamentoItemServico>();
        }
    }
}
