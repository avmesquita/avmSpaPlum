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
    [Table("TB_FORNECEDOR")]
    public class Fornecedor
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_FORNECEDOR", Order = 1)]
        public int CodigoFornecedor { get; set; }

        [Display(Name = "Nome")]
        [Column("TXT_NOME", Order = 2)]
        [Required(ErrorMessage = "Informe o nome do fornecedor")]
        public string Nome { get; set; }

        [Display(Name = "O que fornece?")]
        [Column("TXT_OQUE_FORNECE", Order = 3)]
        [Required(ErrorMessage = "Informe o que este fornecedor fornece")]
        [DataType(DataType.MultilineText)]
        public string ForneceOque { get; set; }

        [Display(Name = "Telefone")]
        [Column("TXT_TELEFONE_1", Order = 4)]
        [DataType(DataType.PhoneNumber)]        
        public string Telefone1 { get; set; }

        [Display(Name = "Telefone")]
        [Column("TXT_TELEFONE_2", Order = 5)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone2 { get; set; }

        [Display(Name = "Telefone")]
        [Column("TXT_TELEFONE_3", Order = 6)]
        [DataType(DataType.PhoneNumber)]
        public string Telefone3 { get; set; }

        [Display(Name = "Celular")]
        [Column("TXT_CELULAR", Order = 7)]
        [DataType(DataType.PhoneNumber)]
        public string Celular { get; set; }

        [Display(Name = "Email")]
        [Column("TXT_EMAIL_PRINCIPAL", Order = 8)]
        [DataType(DataType.EmailAddress)]
        public string EmailPrincipal { get; set; }

        [Display(Name = "Email")]
        [Column("TXT_EMAIL_SECUNDARIO", Order = 9)]
        [DataType(DataType.EmailAddress)]
        public string EmailSecundario { get; set; }


        [Display(Name = "Endereco")]
        [Column("TXT_ENDERECO", Order = 10)]
        [DataType(DataType.MultilineText)]
        public string Endereço { get; set; }

        [Display(Name = "Observação")]
        [Column("TXT_OBSERVACAO", Order =11)]
        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; }

        [Display(Name = "Ativo")]
        [Column("IND_ATIVA", Order = 12)]
        public bool Ativo { get; set; }

        [Display(Name = "Data Cadastro")]
        [Column("DAT_CADASTRO", Order = 13)]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "01/01/1753", "31/12/9999")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime DataCadastro { get; set; }

        [ForeignKey("Filial")]
        [Display(Name = "Filial")]
        [Column("COD_FILIAL", Order = 14)]
        public int CodigoFilial { get; set; }

        public virtual Filial Filial { get; set; }

        public Fornecedor()
        {
            this.Ativo = true;
            this.Celular = "";
            this.CodigoFilial = 0;
            this.CodigoFornecedor = 0;
            this.DataCadastro = DateTime.Now;
            this.EmailPrincipal = "";
            this.EmailSecundario = "";
            this.Endereço = "";            
            this.ForneceOque = "";
            this.Nome = "";
            this.Observacao = "";
            this.Telefone1 = "";
            this.Telefone2 = "";
            this.Telefone3 = "";
            //this.Filial = new Filial();
        }

    }
}