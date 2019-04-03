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
    [Table("TB_LOG")]
    public class Log
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_LOG")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int32 CodigoLog { get; set; }

        [Display(Name = "Data de Registro")]
        [Column("DAT_REGISTRO")]
        [DataType(DataType.DateTime)]
        public DateTime DataRegistro { get; set; }

        [Display(Name = "Filial")]
        [Column("COD_FILIAL")]
        public int? CodigoFilial { get; set; }

        [Display(Name = "Cliente")]
        [Column("COD_CLIENTE")]        
        public int? CodigoCliente { get; set; }

        [Display(Name = "Título")]
        [Column("TXT_TITULO_OCORRENCIA")]        
        public string Titulo { get; set; }

        [Display(Name = "Dados")]
        [Column("TXT_TEXTO_OCORRENCIA")]
        [DataType(DataType.MultilineText)]
        public string Texto { get; set; }

        [Display(Name = "Dados")]
        [Column("TXT_TEXTO_EXCEPTION")]
        [DataType(DataType.MultilineText)]
        public string Erro { get; set; }

        [NotMapped]
        public Exception exception { get; set; }
    }
}