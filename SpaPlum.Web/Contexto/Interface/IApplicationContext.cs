using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Web.Contexto.Interface
{
    public interface IApplicationContext
    {
            string EmailDoFaleConosco { get; }

        /// <summary>
            /// Flag indicando se a aplicação está rodando no ambiente de homologação
            /// </summary>
            bool EhHomologacao { get; }

            string FacebookAppId { get; }

            /// <summary>
            /// Displayname do SMTP
            /// </summary>
            string NomeDeExibicaoDoUsuarioDoSMTP { get; }

            /// <summary>
            /// Username do SMTP
            /// </summary>
            string NomeDoUsuarioDoSMTP { get; }

            /// <summary>
            /// Porta do SMTP
            /// </summary>
            int PortaDoSMTP { get; }

            /// <summary>
            /// Password do SMTP
            /// </summary>
            string SenhaDoUsuarioDoSMTP { get; }

            /// <summary>
            /// Host do SMTP
            /// </summary>
            string ServidorDoSMTP { get; }

            /// <summary>
            /// SSL enable do SMTP
            /// </summary>
            bool SSLDoSMTPHabilitado { get; }

            /// <summary>
            /// Caminho do Servidor
            /// </summary>
            string CaminhoDasImagensDosEmails { get; }

            /// <summary>
            /// Paypal - Permite ativar ou desativar o pagamento por paypal
            /// </summary>
            bool PaypalAtivo { get; }

            /// <summary>
            /// Paypal - Banner com logo para operação na página do paypal
            /// </summary>
            string PaypalLogo { get; }

            /// <summary>
            /// Paypal - Se banner não for informado, aparecerá o texto de BrandName
            /// </summary>
            string PaypalBrandName { get; }

            /// <summary>
            /// DevMail - Define se o e-mail de depuração automático para desenvolvedores está ativo
            /// </summary>
            bool EngineDevMail { get; }

            /// <summary>
            /// Se EngineDevMail = true Então deve ser obrigatório
            /// Trata-se do destino da Mensagem
            /// </summary>
            string DevMailToMail { get; }

            /// <summary>
            /// Se EngineDevMail = true Então deve ser obrigatório
            /// Trata-se do nome do destino da Mensagem
            /// </summary>
            string DevMailToName { get; }

            /// <summary>
            /// Se EngineDevMail = true Então deve ser obrigatório
            /// Trata-se do hostname do servidor SMTP
            /// </summary>
            string DevMailSMTPHost { get; }

            /// <summary>
            /// Se EngineDevMail = true Então deve ser obrigatório
            /// Trata-se da porta do servidor SMTP
            /// </summary>
            int DevMailSMTPPort { get; }

            /// <summary>
            /// Se EngineDevMail = true Então deve ser obrigatório
            /// Indica se a conexão com o servidor SMTP é segura
            /// </summary>
            bool DevMailSMTPSSL { get; }

            /// <summary>
            /// Se EngineDevMail = true Então deve ser obrigatório
            /// Trata-se do usuário usado no servidor SMTP
            /// </summary>
            string DevMailSMTPUsername { get; }

            /// <summary>
            /// Se EngineDevMail = true Então deve ser obrigatório
            /// Trata-se da senha do usuário usado no servidor SMTP
            /// </summary>
            string DevMailSMTPPassword { get; }

            /// <summary>
            /// Retorna a URL onde estão os arquivos estáticos 
            /// </summary>
            string HttpStaticFiles { get; }

            /// <summary>
            /// Permite o envio de mensagens com cópia oculta para depuração e testes
            /// </summary>
            bool EnviarEmailCopiaOculta { get; }

            /// <summary>
            /// Define os emails que receberão uma cópia oculta separados por ";"
            /// </summary>
            string EmailsCopiaOculta { get; }
        }
}