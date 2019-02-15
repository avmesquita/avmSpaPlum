using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Entity
{
    [Serializable]
    [Table("TB_EMAIL")]
    public class Email
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_EMAIL")]
        public int CodigoEmail { get; set; }

        [Display(Name = "Do Nome")]
        [Column("TXT_FROM_NAME")]        
        public string DoNome { get; set; }


        [Display(Name = "Do Email")]
        [Column("TXT_FROM_EMAIL")]
        public string DoEmail { get; set; }

        [Display(Name = "Do Nome")]
        [Column("TXT_TO_NAME")]
        public string ParaNome { get; set; }

        [Display(Name = "Do Email")]
        [Column("TXT_TO_EMAIL")]
        public string ParaEmail { get; set; }

        [Display(Name = "Assunto")]
        [Column("TXT_ASSUNTO")]
        public string Assunto { get; set; }

        [Display(Name = "Mensagem")]
        [Column("TXT_MENSAGEM")]
        public string Mensagem { get; set; }

        [Display(Name = "Data do Envio")]
        [Column("DAT_ENVIO")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm:ss}")]
        public DateTime? DataEnvio { get; set; }

        [Display(Name = "Extra")]
        [Column("TXT_EXTRA")]
        public string Extra { get; set; }

        [Display(Name = "Email foi lido?")]
        [Column("IND_FOI_LIDO")]
        public bool FoiLido { get; set; }

        [Display(Name = "Houve falha?")]
        [Column("IND_HOUVE_FALHA")]
        public bool HouveFalha { get; set; }


        [Display(Name = "Agendamento")]
        [Column("COD_AGENDAMENTO")]
        public int? CodigoAgendamento { get; set; }

		[NotMapped]
		public List<System.Net.Mail.Attachment> ListaDeAnexos { get; set; }

        public Email()
        {
            this.Assunto = "";
            this.CodigoAgendamento = 0;
            this.CodigoEmail = 0;
            this.DataEnvio = null;
            this.DoEmail = "";
            this.DoNome = "";
            this.Extra = "";
            this.FoiLido = false;
            this.HouveFalha = false;
            this.Mensagem = "";
            this.ParaEmail = "";
            this.ParaNome = "";

			this.ListaDeAnexos = new List<System.Net.Mail.Attachment>();
        }


    }
}