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
    [Table("TB_TERAPEUTA")]
    public class Terapeuta
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_TERAPEUTA", Order = 1)]
        public int CodigoTerapeuta { get; set; }

        [Display(Name = "Nome")]
        [Column("TXT_NOME", Order = 3)]
        [Required(ErrorMessage = "Informe o nome do funcionário")]
        public string Nome { get; set; }

		[Display(Name = "E-Mail")]
		[Column("TXT_EMAIL", Order = 6)]
		[DataType(DataType.EmailAddress)]
		[Required(ErrorMessage = "Informe o email do funcionário")]
		public string EMail { get; set; }

		[Display(Name = "Telefone")]
		[Column("TXT_TELEFONE", Order = 7)]
		[DataType(DataType.PhoneNumber)]
		public string Telefone { get; set; }

		[Display(Name = "Ativa")]
        [Column("IND_ATIVA", Order = 4)]
        public bool Ativa { get; set; }

        [Display(Name = "Data Cadastro")]
        [Column("DAT_CADASTRO", Order = 5)]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "01/01/1753", "31/12/9999")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy hh:mm}")]
        public DateTime DataCadastro { get; set; }

        [ForeignKey("Filial")]
        [Display(Name = "Filial")]
        [Column("COD_FILIAL", Order = 2)]
        public int CodigoFilial { get; set; }

        public virtual Filial Filial { get; set; }

        public virtual ICollection<HorarioTerapeuta> Horarios { get; set; }
        public virtual ICollection<PontoTerapeuta> FolhaDePonto { get; set; }
        public virtual ICollection<TerapeutaServico> Servicos { get; set; }

        public Terapeuta()
        {
            this.Ativa = true;
            this.CodigoFilial = 0;
            this.CodigoTerapeuta = 0;
            this.DataCadastro = DateTime.Now;            
            this.Nome = string.Empty;
			this.EMail = string.Empty;

            this.Horarios = new List<HorarioTerapeuta>();
            this.Servicos = new List<TerapeutaServico>();
            this.FolhaDePonto = new List<PontoTerapeuta>();

        }

    }
}
