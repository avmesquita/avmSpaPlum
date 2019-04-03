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
    [Table("TB_PERFIL_ACESSO")]
    public class PerfilAcesso
    {
        [Key]
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		[Display(Name = "Código")]
        [Column("COD_PERFIL_ACESSO")]
        public int CodigoPerfilAcesso { get; set; }

        [Display(Name = "Descrição")]
        [Column("TXT_DESCRICAO")]
        [Required(ErrorMessage = "Informe o nome do perfil de acesso")]
        public string Descricao { get; set; }

        [Display(Name = "Ativo")]
        [Column("IND_ATIVO")]
        public bool Ativo { get; set; }

        public PerfilAcesso()
        {
            this.Ativo = true;
            this.CodigoPerfilAcesso = 0;
            this.Descricao = "";            
        }
    }
}
