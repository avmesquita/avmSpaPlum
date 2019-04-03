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
	[Table("TB_EMPRESA")]
	public class Empresa
	{
		[Key]
		[Display(Name = "Código")]
		[Column("COD_EMPRESA", Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CodigoEmpresa { get; set; }

		[Display(Name = "Nome")]
		[Column("TXT_NOME", Order = 2)]
		[Required(ErrorMessage = "Informe o nome fantasia")]
		public string Nome { get; set; }

		[Display(Name = "Razao Social")]
		[Column("TXT_RAZAO_SOCIAL", Order = 3)]
		[Required(ErrorMessage = "Informe a razão social")]
		public string RazaoSocial { get; set; }

		[Display(Name = "CNPJ")]
		[Column("TXT_CNPJ", Order = 4)]
		[Required(ErrorMessage = "Informe o CNPJ")]
		public string CNPJ { get; set; }

		[Display(Name = "E-Mail")]
		[Column("TXT_EMAIL_PRINCIPAL", Order = 5)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Display(Name = "Telefone")]
		[Column("TXT_TELEFONE", Order = 6)]
		public string Telefone { get; set; }

		[Display(Name = "Ativo")]
		[Column("IND_ATIVO", Order = 7)]
		public bool Ativo { get; set; }

		[Display(Name = "Data de Cadastramento")]
		[Column("DAT_CADASTRO", Order = 9)]
		[DataType(DataType.DateTime)]
		public DateTime DataCadastro { get; set; }

		[Display(Name = "Plano")]
		[Column("COD_PLANO", Order = 8)]
		public int CodigoPlano { get; set; }

		[ForeignKey("CodigoPlano")]
		public virtual Plano Plano { get; set; }

		// E-MAILS
		public virtual ICollection<Filial> Filiais { get; set; }

		public Empresa()
		{
			this.CodigoEmpresa = 0;
			this.Nome = string.Empty;
			this.RazaoSocial = string.Empty;
			this.CNPJ = string.Empty;
			this.Email = string.Empty;
			this.Telefone = string.Empty;
			this.Ativo = true;
			this.DataCadastro = DateTime.Now;

			this.Filiais = new List<Filial>();
			this.Plano = new Plano();
		}

	}
}