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
    [Table("TB_PONTO_TERAPEUTA")]
    public class PontoTerapeuta
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_PONTO", Order = 1)]
        public Int32 CodigoPonto { get; set; }

        [ForeignKey("Terapeuta")]
        [Display(Name = "Profissional")]
        [Column("COD_TERAPEUTA", Order = 2)]
        public int? CodigoTerapeuta { get; set; }

        [ForeignKey("Cliente")]
        [Display(Name = "Cliente")]
        [Column("COD_CLIENTE", Order = 3)]
        public int? CodigoCliente { get; set; }

        [Display(Name = "Data Marcação")]
        [Column("DAT_MARCACAO", Order = 4)]        
        public DateTime Data { get; set; }

        public virtual Terapeuta Terapeuta { get; set; }
        public virtual Cliente Cliente { get; set; }

		public PontoTerapeuta()
		{
			this.Data = DateTime.Now;
		}
    }
}
