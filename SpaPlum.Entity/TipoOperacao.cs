using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPlum.Entity
{
    public enum TipoOperacao
    {
        Nenhum = 0,
        Credito = 1,
        Debito = 2
        //,Transferencia = 3
    }

    /*
    [Serializable]
    [Table("TB_TIPO_OPERACAO")]
    public class TipoOperacao
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_TIPO_OPERACAO")]
        public int CodigoTipoOperacao { get; set; }

        [Display(Name = "Descrição")]
        [Column("TXT_DESCRICAO")]
        [Required(ErrorMessage = "Informe o nome do status para o agendamento")]
        public string Descricao { get; set; }

        [Display(Name = "Ativo")]
        [Column("IND_ATIVO")]               
        public bool Ativo { get; set; }

        public virtual ICollection<Lancamento> Lancamentos { get; set; }

        public TipoOperacao()
        {
            this.Ativo = true;
            this.CodigoTipoOperacao = 0;
            this.Descricao = "";

            this.Lancamentos = new List<Lancamento>();
        }
    }*/

}
