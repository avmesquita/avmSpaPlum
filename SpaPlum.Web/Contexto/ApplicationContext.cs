using SpaPlum.Web.Contexto.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace SpaPlum.Web.Contexto
{
	public class ApplicationContext : IApplicationContext
	{
		private const string EH_HOMOLOGACAO = "IsHomolog";
		private const string CAMINHO_DAS_IMAGENS_DOS_EMAILS = "CaminhoDasImagensDosEmails";

		private const string FACEBOOK_APP_ID = "FacebookAppID";

		#region FUNÇÕES ADMINISTRATIVAS
		// 0 - Development
		private const string MAIN_EMAILS = "SEGURANCA.MainEmails";
		// 1 - Administrador | 2 - Cliente | 3 - Profissional (Ponto) | 4 - Gerente
		private const string E_MAILS_FUNCAO_ADMINISTRADOR = "SEGURANCA.FuncaoAdministrador";
		private const string E_MAILS_FUNCAO_CLIENTE = "SEGURANCA.FuncaoCliente";
		private const string E_MAILS_FUNCAO_FUNCIONARIO = "SEGURANCA.FuncaoFuncionario";
		private const string E_MAILS_FUNCAO_GERENTE = "SEGURANCA.FuncaoGerente";
		#endregion

		#region SMTP
		private const string CAMINHO_DA_IMAGEM_DO_PRODUTO = "SMTP.ProductPicturePath";
		private const string NOME_DE_EXIBICAO_DO_USUARIO_SMTP = "SMTP.DisplayName";
		private const string NOME_DO_USUARIO_SMTP = "SMTP.Username";
		private const string PORTA_DO_SMTP = "SMTP.Port";
		private const string SENHA_DO_USUARIO_DO_SMTP = "SMTP.Password";
		private const string SERVIDOR_DO_SMTP = "SMTP.Host";
		private const string SSL_DO_SMTP_HABILITADO = "SMTP.EnableSSL";
		#endregion

		#region FALE CONOSCO
		private const string CAMINHO_DO_TEMPLATE_DE_EMAIL = "EmailTemplatePath";
		private const string EMAIL_DO_FALE_CONOSCO = "EmailFaleConosco";
		#endregion

		#region PAYPAL
		private const string PAYPAL_ATIVO = "Paypal.Ativo";
		private const string PAYPAL_LOGO = "Paypal.Logo";
		private const string PAYPAL_BRAND_NAME = "Paypal.BrandName";
		#endregion

		#region DEVMAIL
		private const string ENGINE_DEVMAIL = "Engine.DevMail";
		private const string DEVMAIL_TOMAIL = "DevMail.ToMail";
		private const string DEVMAIL_TONAME = "DevMail.ToName";
		private const string DEVMAIL_SMTPHOST = "DevMail.SMTPHost";
		private const string DEVMAIL_SMTPPORT = "DevMail.SMTPPort";
		private const string DEVMAIL_SMTPSSL = "DevMail.SMTPSsl";
		private const string DEVMAIL_SMTPUSERNAME = "DevMail.SMTPUsername";
		private const string DEVMAIL_SMTPPASSWORD = "DevMail.SMTPPassword";
		#endregion

		#region EMAILCOPIAOCULTA
		private const string EMAILBCC = "Debug.EmailCopiaOculta";
		private const string EMAILBCCACCOUNTS = "Debug.EmailCopiaOculta.Accounts";
		#endregion

		#region CDN
		private const string HTTP_STATIC_FILES = "HttpStaticFiles";
		#endregion

		#region PRIVATE METHODS
		private static string GetValueFromContext(string key)
		{
			return WebConfigurationManager.AppSettings[key];
		}
		#endregion

		#region PUBLIC METHODS        
		public string CaminhoDaImagemDoProduto
		{
			get
			{
				return string.Concat(HttpContext.Current.Server.MapPath("~"), GetValueFromContext(CAMINHO_DA_IMAGEM_DO_PRODUTO));
			}
		}

		public string CaminhoDoTemplateDeEmail
		{
			get
			{
				return string.Concat(HttpContext.Current.Server.MapPath("~"), GetValueFromContext(CAMINHO_DO_TEMPLATE_DE_EMAIL));
			}
		}

		public string EmailDoFaleConosco
		{
			get
			{
				return GetValueFromContext(EMAIL_DO_FALE_CONOSCO);
			}
		}

		public string FacebookAppId
		{
			get
			{
				return GetValueFromContext(FACEBOOK_APP_ID);
			}
		}

		public string MainEmails
		{
			get
			{
				return GetValueFromContext(MAIN_EMAILS);
			}
		}

		public string EmailsFuncaoAdministrador
		{
			get
			{
				return GetValueFromContext(E_MAILS_FUNCAO_ADMINISTRADOR);
			}
		}

		public string EmailsFuncaoCliente
		{
			get
			{
				return GetValueFromContext(E_MAILS_FUNCAO_CLIENTE);
			}
		}

		public string EmailsFuncaoFuncionario
		{
			get
			{
				return GetValueFromContext(E_MAILS_FUNCAO_FUNCIONARIO);
			}
		}

		public string EmailsFuncaoGerente
		{
			get
			{
				return GetValueFromContext(E_MAILS_FUNCAO_GERENTE);
			}
		}

		public string NomeDeExibicaoDoUsuarioDoSMTP
        {
            get
            {
                return GetValueFromContext(NOME_DE_EXIBICAO_DO_USUARIO_SMTP);
            }
        }

        public string NomeDoUsuarioDoSMTP
        {
            get
            {
                return GetValueFromContext(NOME_DO_USUARIO_SMTP);
            }
        }

        public int PortaDoSMTP
        {
            get
            {
                return Convert.ToInt32(GetValueFromContext(PORTA_DO_SMTP));
            }
        }

        public string SenhaDoUsuarioDoSMTP
        {
            get
            {
                return GetValueFromContext(SENHA_DO_USUARIO_DO_SMTP);
            }
        }

        public string ServidorDoSMTP
        {
            get
            {
                return GetValueFromContext(SERVIDOR_DO_SMTP);
            }
        }

        public bool SSLDoSMTPHabilitado
        {
            get
            {
                return Convert.ToBoolean(GetValueFromContext(SSL_DO_SMTP_HABILITADO));
            }
        }

        public bool EhHomologacao
        {
            get { return Convert.ToBoolean(GetValueFromContext(EH_HOMOLOGACAO)); }
        }

        public string CaminhoDasImagensDosEmails
        {
            get { return GetValueFromContext(CAMINHO_DAS_IMAGENS_DOS_EMAILS); }
        }

        public string PaypalLogo
        {
            get { return GetValueFromContext(PAYPAL_LOGO); }
        }

        public string PaypalBrandName
        {
            get { return GetValueFromContext(PAYPAL_BRAND_NAME); }
        }

        public bool PaypalAtivo
        {
            get { return Convert.ToBoolean(GetValueFromContext(PAYPAL_ATIVO)); }
        }

        public bool EngineDevMail
        {
            get { return Convert.ToBoolean(GetValueFromContext(ENGINE_DEVMAIL)); }
        }

        public string DevMailToMail
        {
            get { return GetValueFromContext(DEVMAIL_TOMAIL); }
        }

        public string DevMailToName
        {
            get { return GetValueFromContext(DEVMAIL_TONAME); }
        }

        public string DevMailSMTPHost
        {
            get { return GetValueFromContext(DEVMAIL_SMTPHOST); }
        }

        public int DevMailSMTPPort
        {
            get { return Convert.ToInt32(GetValueFromContext(DEVMAIL_SMTPPORT)); }
        }

        public bool DevMailSMTPSSL
        {
            get { return Convert.ToBoolean(GetValueFromContext(DEVMAIL_SMTPSSL)); }
        }

        public string DevMailSMTPUsername
        {
            get { return GetValueFromContext(DEVMAIL_SMTPUSERNAME); }
        }

        public string DevMailSMTPPassword
        {
            get { return GetValueFromContext(DEVMAIL_SMTPPASSWORD); }
        }

        public string HttpStaticFiles
        {
            get { return GetValueFromContext(HTTP_STATIC_FILES); }
        }

        public bool EnviarEmailCopiaOculta
        {
            get { return Convert.ToBoolean(GetValueFromContext(EMAILBCC)); }
        }

        public string EmailsCopiaOculta
        {
            get { return GetValueFromContext(EMAILBCCACCOUNTS); }
        }

        #endregion

    }
}