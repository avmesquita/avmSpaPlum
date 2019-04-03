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
    [Table("TB_FILIAL")]
    public class Filial
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_FILIAL", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoFilial { get; set; }

        [Display(Name = "Nome")]
        [Column("TXT_NOME", Order = 2)]
        [Required(ErrorMessage = "Informe o nome da filial")]
        public string Nome { get; set; }

		[Display(Name = "Empresa")]
		[Column("COD_EMPRESA", Order = 3)]
		public int CodigoEmpresa { get; set; }

		[ForeignKey("CodigoEmpresa")]
		public virtual Empresa Empresa { get; set; }

		[Display(Name = "Ativo")]
        [Column("IND_ATIVO", Order = 4)]
        public bool Ativo { get; set; }

        // E-MAILS

        [Display(Name ="E-Mail Principal")]
        [Column("TXT_EMAIL_PRINCIPAL",Order = 5)]
        public string EmailPrincipalFilial { get; set; }

        [Display(Name = "E-Mail Secundario")]
        [Column("TXT_EMAIL_SECUNDARIO", Order = 6)]
        public string EmailSecundarioFilial { get; set; }

        public virtual ICollection<Servico> Servicos { get; set; }
        public virtual ICollection<Agendamento> Agendamentos { get; set; }
        public virtual ICollection<Terapeuta> Terapeutas { get; set; }
        public virtual ICollection<Fornecedor> Fornecedores { get; set;  }
        public virtual ICollection<FilialConfiguracao> Configuracao { get; set; }
        public virtual ICollection<TipoPagamento> FormasPagamento { get; set; }
        public virtual ICollection<Promocao> Promocoes { get; set; }

        public Filial()
        {
            this.Ativo = true;
            this.CodigoFilial = 0;
            this.Nome = "";
            this.EmailPrincipalFilial = "";
            this.EmailSecundarioFilial = "";

            Servicos = new List<Servico>();
            Agendamentos = new List<Agendamento>();
            Terapeutas = new List<Terapeuta>();
            Fornecedores = new List<Fornecedor>();
            Configuracao = new List<FilialConfiguracao>();
            FormasPagamento = new List<TipoPagamento>();
            Promocoes = new List<Promocao>();
        }
    }
}