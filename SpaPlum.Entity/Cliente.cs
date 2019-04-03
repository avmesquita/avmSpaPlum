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
	[Table("TB_CLIENTE")]
	public class Cliente
	{
		[Key]
		[Display(Name = "Código")]
		[Column("COD_CLIENTE")]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CodigoCliente { get; set; }

		[ForeignKey("PerfilAcesso")]
		[Display(Name = "Perfil de Acesso")]
		[Column("COD_PERFIL_ACESSO")]
		public int CodigoPerfilAcesso { get; set; }

		public virtual PerfilAcesso PerfilAcesso { get; set; }

		[Display(Name = "Nome")]
		[Column("TXT_NOME")]
		[Required(ErrorMessage = "Informe o nome do cliente")]
		public string Nome { get; set; }

		[Display(Name = "E-Mail")]
		[Column("TXT_EMAIL")]
		[DataType(DataType.EmailAddress)]
	    [StringLength(150)]
		[Index(IsUnique=true)]		
		[Required(ErrorMessage = "Informe o e-mail do cliente")]
		public string Email { get; set; }

		[Display(Name = "Telefone")]
		[Column("TXT_TELEFONE")]
		[DataType(DataType.PhoneNumber)]
		//[Required(ErrorMessage = "Informe o telefone do cliente")]
		public string Telefone { get; set; }

		[Display(Name = "TokenVIP")]
		[Column("TXT_TOKEN_VIP")]
		public string TokenVIP { get; set; }

		[Display(Name = "Data de Cadastro")]
		[Column("DAT_CADASTRO")]
		[DataType(DataType.DateTime)]
		public DateTime DataCadastro { get; set; } = DateTime.Now;

		[Display(Name = "Ativo")]
		[Column("IND_ATIVO")]
		public bool Ativo { get; set; } = true;

		[Display(Name = "Pontos VIP")]
		[Column("NUM_PONTOS_VIP")]
		public int PontosVIP { get; set; }

		[Display(Name = "Senha")]
		[NotMapped]
		[DataType(DataType.Password)]
		[Required]
		public string Senha { get; set; }

		[Display(Name = "Confirmar Senha")]
		[NotMapped]
		[DataType(DataType.Password)]
		[Required]
		public string ConfirmarSenha { get; set; }

		/// <summary>
		/// Estou usando como atributo alterado para forçar o migrations em modo de preguiçoso.
		/// Alterar o nome da coluna a cada vez... rs.
		/// </summary>
		//[Column("IND_MAGIC_MIGRATIONS")]
		//public bool AttributeToForceMigrationsRecreateAll { get; set; }

		public Cliente()
		{
			this.CodigoCliente = 0;
			this.Nome = "";
			this.Ativo = true;
			this.Email = "";
			this.Telefone = "";
			this.DataCadastro = DateTime.Now;
			this.TokenVIP = "";
			this.PontosVIP = 0;

			this.PerfilAcesso = new PerfilAcesso();

			this.Senha = string.Empty;
			this.ConfirmarSenha = string.Empty;
		}
	}

}