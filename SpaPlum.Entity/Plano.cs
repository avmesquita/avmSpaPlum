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
	[Table("TB_PLANO")]
	public class Plano
	{
		[Key]
		[Display(Name = "Código")]
		[Column("COD_PLANO", Order = 1)]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int CodigoPlano { get; set; }

		[Display(Name = "Nome")]
		[Column("TXT_NOME", Order = 2)]
		[Required(ErrorMessage = "Informe o nome da comercial")]
		public string Nome { get; set; }

		[Display(Name = "Informe a descrição interna do plano")]
		[Column("TXT_DESCRICAO", Order = 3)]
		public string Descricao { get; set; }

		[Display(Name = "Inicio da Vigência")]
		[Column("DAT_INICIO_VIGENCIA", Order = 4)]
		public DateTime VigenciaInicio { get; set; }

		[Display(Name = "Fim da Vigência")]
		[Column("DAT_FIM_VIGENCIA", Order = 5)]
		public DateTime? VigenciaFim { get; set; }

		[Display(Name = "Preço")]
		[Column("VAL_PRECO", Order = 6)]
		public Decimal Preco { set; get; }

		public Plano()
		{
			this.CodigoPlano = 0;
			this.Nome = string.Empty;
			this.Descricao = string.Empty;
			this.VigenciaInicio = DateTime.Now;
			this.VigenciaFim = null;
			this.Preco = 0;
		}

	}
}