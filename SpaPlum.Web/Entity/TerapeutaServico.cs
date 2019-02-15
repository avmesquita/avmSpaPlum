using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Entity
{
    /// <summary>
    /// Entidade responsável por ter os SERVICOS que a TERAPEUTA realiza
    /// </summary>
    [Serializable]
    [Table("TB_TERAPEUTA_SERVICO")]
    public class TerapeutaServico
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_TERAPEUTA_SERVICO")]
        public int CodigoTerapeutaServico { get; set; }

        // FK TERAPEUTA

        [Display(Name = "Terapeuta")]
        [Column("COD_TERAPEUTA")]
        public int CodigoTerapeuta { get; set; }

        [ForeignKey("CodigoTerapeuta")]
        public Terapeuta Terapeuta { get; set; }

        // FK SERVICOS

        [Display(Name = "Servico")]
        [Column("COD_SERVICO")]
        public int CodigoServico { get; set; }
        
        [ForeignKey("CodigoServico")]
        public Servico Servico { get; set; }

        [NotMapped]
        [Display(Name = "Seleção de Serviços")]
        public System.Web.Mvc.MultiSelectList ServicosSelecionados { get; set; }

        [NotMapped]
        public List<string> ServicosIds { get; set; }

		[NotMapped]
		public virtual ICollection<Terapeuta> Terapeutas { get; set; }

		[NotMapped]
		public virtual ICollection<Servico> Servicos { get; set; }

        public TerapeutaServico()
        {
            this.CodigoTerapeutaServico = 0;
            this.CodigoTerapeuta = 0;
            this.CodigoServico = 0;

            this.Servicos = new List<Servico>();
            this.Terapeutas = new List<Terapeuta>();

            this.ServicosIds = new List<string>();

            //this.ServicosSelecionados = new System.Web.Mvc.MultiSelectList({ new System.Web.Mvc.SelectListItem() { Text = "", Value = "0", Selected = false, Group = null } });

        }

    }
}