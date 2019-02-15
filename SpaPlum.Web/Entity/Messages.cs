using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SpaPlum.Web.Entity
{
    [Serializable]
    [Table("TB_CHAT_MENSAGEM")]
    public class Mensagem
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_MENSAGEM")]
        public int CodigoMensagem { get; set; }

        [Display(Name = "Usuário")]
        [Column("TXT_USUARIO")]
        [MaxLength(20)]
        public string Username { get; set; }

        [Display(Name = "Data")]
        [Column("DAT_POST")]
        public DateTime DataPost { get; set; }

        [Display(Name = "Mensagem")]
        [Column("TXT_MENSAGEM")]
        public string CorpoMensagem { get; set; }


    }
}