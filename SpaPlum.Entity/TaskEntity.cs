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
    [Table("TB_TAREFA")]
    public class TaskEntity
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_TAREFA")]
        public int Codigo { get; set; }

        [Display(Name = "Nome")]
        [Column("TXT_NOME")]
        [Required(ErrorMessage = "Informe o Nome")]                
        public string Nome { get; set; }

        [Display(Name = "Categoria")]
        [Column("COD_CATEGORIA")]
        [Required(ErrorMessage = "Informe a Categoria")]
        public string Categoria { get; set; }

        [Display(Name = "Data")]
        [Column("DAT_TAREFA")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Informe a data da Tarefa")]
        public DateTime Date { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Column("DAT_Cadastro")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }
    }

    // Usada no protótipo
    [Serializable]
    [Table("TB_PROTOTIPO")]
    public class MyTask
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_TAREFA")]
        public int Codigo { get; set; }

        [Display(Name = "Nome")]
        [Column("TXT_NOME")]
        [Required(ErrorMessage = "Informe o Nome")]
        public string Nome { get; set; }

        [Display(Name = "Categoria")]
        [Column("COD_CATEGORIA")]
        [Required(ErrorMessage = "Informe a Categoria")]
        public string Categoria { get; set; }

        [Display(Name = "Data")]
        [Column("DAT_TAREFA")]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Informe a data da Tarefa")]
        public DateTime Date { get; set; }

        [Display(Name = "Data de Cadastro")]
        [Column("DAT_Cadastro")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }
    }

}
