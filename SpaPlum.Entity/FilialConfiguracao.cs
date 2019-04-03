using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SpaPlum.Entity
{
    [Serializable]
    [Table("TB_FILIAL_CONFIGURACAO")]
    public class FilialConfiguracao
    {
        [Key]
        [Display(Name = "Código")]
        [Column("COD_CONFIGURACAO", Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CodigoConfiguracao { get; set; }

        // RELACIONAMENTO com FILIAL

        [Display(Name = "Filial")]
        [Column("COD_FILIAL", Order = 2)]
        public int CodigoFilial { get; set; }

        [ForeignKey("CodigoFilial")]
        public virtual Filial Filial { get; set; }

        // CFG - ENVIO DE E-MAILS

        [Display(Name = "Servidor de E-Mails")]
        [Column("TXT_SMTP_HOST", Order = 3)]
        public string EmailSmtpHost { get; set; }

        [Display(Name = "Porta do Servidor")]
        [Column("TXT_SMTP_PORT", Order = 4)]
        public int EmailSmtpPort { get; set; }

        [Display(Name = "SSL Ativo")]
        [Column("TXT_SMTP_SSL", Order = 5)]
        public bool EmailSmtpSSL { get; set; }

        [Display(Name = "Conta de E-Mail")]
        [Column("TXT_SMTP_ACCOUNT", Order = 6)]
        public string EmailSmtpAccount { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Column("TXT_SMTP_PASSWORD", Order = 7)]
        public string EmailSmtpPassword { get; set; }

        [Display(Name = "Ativa")]
        [Column("IND_ATIVA", Order = 8)]
        public bool Ativa { get; set; }

        [Display(Name = "")]
        [Column("DAT_CONFIGURACAO", Order = 9)]
        public DateTime DataCadastro { get; set; }

        public FilialConfiguracao()
        {
            this.CodigoConfiguracao = 0;
            this.CodigoFilial = 0;
            this.EmailSmtpAccount = "";
            this.EmailSmtpHost = "";
            this.EmailSmtpPassword = "";
            this.EmailSmtpPort = 0;
            this.EmailSmtpSSL = false;
            this.Ativa = true;
            this.DataCadastro = DateTime.Now;
        }
    }
}