using Microsoft.AspNet.Identity;
using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;
using SpaPlum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;

namespace SpaPlum.Web.Service
{
    public class Mailer
    {
        string _emailPrincipalCorpusSPA = WebConfigurationManager.AppSettings["EmailPrincipalCorpusSPA"];
        string _emailSecundarioCorpusSPA = WebConfigurationManager.AppSettings["EmailSecundarioCorpusSPA"];
        string _smtpFromAddress = WebConfigurationManager.AppSettings["SmtpFromAddress"];
        string _smtpServerName = WebConfigurationManager.AppSettings["SmtpServerName"];
        string _smtpPort = WebConfigurationManager.AppSettings["SmtpPort"];
        string _smtpUserName = WebConfigurationManager.AppSettings["SmtpUserName"];
        string _smtpPassword = WebConfigurationManager.AppSettings["SmtpPassword"];
        string _smtpSSL = WebConfigurationManager.AppSettings["SmtpSsl"];

        public bool EnviarEmail(Email email)
        {
            try
            {
                MailMessage mensagem = new MailMessage();
                mensagem.To.Add(new MailAddress(_emailPrincipalCorpusSPA, "Corpus SPA"));

                if (!string.IsNullOrEmpty(_emailSecundarioCorpusSPA))
                {
                    mensagem.To.Add(new MailAddress(_emailSecundarioCorpusSPA, "Corpus SPA"));
                }
                mensagem.Subject = "[CORPUS SPA - " + email.Assunto + "]";

                mensagem.Body = email.Mensagem;
                mensagem.From = new MailAddress(_emailPrincipalCorpusSPA, "Corpus SPA");
                mensagem.ReplyToList.Add(new MailAddress(_emailPrincipalCorpusSPA, "Corpus SPA"));
                mensagem.IsBodyHtml = true;
				// Anexa a lista de anexos do objeto de e-mail
				foreach (var mailAnexo in email.ListaDeAnexos)
				{
					mensagem.Attachments.Add(mailAnexo);
				}

                SmtpClient cliente = new SmtpClient();
                cliente.Credentials = new NetworkCredential(_smtpUserName, _smtpPassword);
                cliente.Host = _smtpServerName;
                cliente.Port = Convert.ToInt32(_smtpPort);
                cliente.EnableSsl = Convert.ToBoolean(_smtpSSL);

                // Trata os certificados do Servidor SSL TLS que pode ser self-signed. Esta rotina permite esta assinatura.
                ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;

                cliente.Send(mensagem);

                // Persistência
                //var agendamento = converterModelParaEntity(model);
                //dal.CreateTask(agendamento);

                return true;
            }
            catch
            {
                return false;
            }
        }

		public bool EnviarEmailAgendamento(Email email)
		{
			try
			{
				email.Mensagem = "DADOS DO AGENDAMENTO:<br><br><br>" +
					              email.Mensagem;
				try
				{
					// INCLUI O ARQUIVO .ICS DE CALENDÁRIO COMO ANEXO NO E-MAIL
					var anexo = obterApontamento(Convert.ToInt32(email.CodigoAgendamento), email.Mensagem);
					email.ListaDeAnexos.Add(anexo);
				}
				catch { }

				return EnviarEmail(email);
			}
			catch
			{
				return false;
			}
		}


		public bool EnviarEmail(FaleConoscoModel model)
        {
            try
            {
                MailMessage mensagem = new MailMessage();
                mensagem.To.Add(new MailAddress("contato@avmsistemas.net", "AVM Sistemas"));
                mensagem.Subject = "[SPAPLUM - CORPUS SPA - CONTATO]";
                mensagem.Body = "NOME: " + model.Nome + "<BR /><BR />" +
                                "E-MAIL: " + model.Email + "<BR /><BR />" +
                                "MENSAGEM:" + "<BR /><BR />" +
                                model.Mensagem;
                mensagem.From = new MailAddress("andre.mesquita.impeto@gmail.com", "SPA Plum SaS");
                mensagem.IsBodyHtml = true;

                SmtpClient cliente = new SmtpClient();
                cliente.Credentials = new NetworkCredential("andre.mesquita.impeto@gmail.com", "impeto.mesquita.andre");
                cliente.Host = "smtp.gmail.com";
                cliente.Port = 587;
                cliente.EnableSsl = true;

                // Trata os certificados do Servidor SSL TLS que pode ser self-signed. Esta rotina permite esta assinatura.
                ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;

                cliente.Send(mensagem);

                // Persistência
                //var agendamento = converterModelParaEntity(model);
                //dal.CreateTask(agendamento);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Send(MailAddress toAddress, string subject, string body, bool priority)
        {
            Task.Factory.StartNew(() => SendEmail(toAddress, subject, body, priority), TaskCreationOptions.LongRunning);
        }

        public Task EnviarEmail(IdentityMessage message)
        {
            try
            {
                MailMessage mensagem = new MailMessage();
                mensagem.To.Add(new MailAddress(message.Destination,message.Destination));
                mensagem.Subject = "[SPAPLUM - CORPUS SPA] " + message.Subject;
                mensagem.Body = message.Body;
                mensagem.From = new MailAddress("andre.mesquita.impeto@gmail.com", "SPA Plum SaS");
                mensagem.IsBodyHtml = true;

                SmtpClient cliente = new SmtpClient();
                cliente.Credentials = new NetworkCredential("andre.mesquita.impeto@gmail.com", "impeto.mesquita.andre");
                cliente.Host = "smtp.gmail.com";
                cliente.Port = 587;
                cliente.EnableSsl = true;

                // Trata os certificados do Servidor SSL TLS que pode ser self-signed. Esta rotina permite esta assinatura.
                ServicePointManager.ServerCertificateValidationCallback = CertificateValidationCallBack;

                cliente.Send(mensagem);

                return Task.FromResult(0);
            }
            catch
            {
                return Task.FromResult(0);
            }            
        }
    

    private Attachment obterApontamento(int codigoAgendamento, string mensagem)
        {
            if (codigoAgendamento != null && codigoAgendamento > 0)
            {

                AgendamentoContexto contexto = new AgendamentoContexto();

                var agendamento = contexto.AgendamentoModels.Where(t => t.CodigoAgendamento == codigoAgendamento).FirstOrDefault();
                var filial = contexto.FilialModels.Where(t => t.CodigoFilial == agendamento.CodigoFilial).FirstOrDefault();

                string fileName = string.Empty;

                AppointmentModel appointment = new AppointmentModel();

                string Assunto = "[CORPUS SPA - TERAPIA MARCADA]";
                string Local = filial.Nome;
                DateTime DataInicial = agendamento.DataInicial;
                DateTime DataFinal = Convert.ToDateTime(agendamento.DataFinal);

                string msg = mensagem;

                var uid = agendamento.Autenticacao;

                fileName = appointment.MakeDayEvent(Assunto,
                                                    Local,
                                                    msg,
                                                    uid,
                                                    DataInicial,
                                                    DataFinal);

                Attachment attachment = new Attachment(fileName);

                return attachment;
            }
            return null;
        }


        private void SendEmail(MailAddress toAddress, string subject, string body, bool priority)
        {
            MailAddress fromAddress = new MailAddress(WebConfigurationManager.AppSettings["SmtpFromAddress"]);
            string serverName = WebConfigurationManager.AppSettings["SmtpServerName"];
            int port = Convert.ToInt32(WebConfigurationManager.AppSettings["SmtpPort"]);
            string userName = WebConfigurationManager.AppSettings["SmtpUserName"];
            string password = WebConfigurationManager.AppSettings["SmtpPassword"];

            var message = new MailMessage(fromAddress, toAddress);

            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = true;
            message.HeadersEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            if (priority) message.Priority = MailPriority.High;

            Thread.Sleep(1000);

            SmtpClient client = new SmtpClient(serverName, port);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = Convert.ToBoolean(WebConfigurationManager.AppSettings["SmtpSsl"]);
            client.UseDefaultCredentials = false;

            NetworkCredential smtpUserInfo = new NetworkCredential(userName, password);
            client.Credentials = smtpUserInfo;

            client.Send(message);

            client.Dispose();
            message.Dispose();
        }


        /// <summary>
        /// Este método, segundo a Microsoft, permite com que mensagens sejam enviadas por SSL 
        /// mesmo quando o certificado do servidor não for assinado.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        private static bool CertificateValidationCallBack(
                 object sender,
                 System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                 System.Security.Cryptography.X509Certificates.X509Chain chain,
                 System.Net.Security.SslPolicyErrors sslPolicyErrors)
        {
            // If the certificate is a valid, signed certificate, return true.
            if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
            {
                return true;
            }

            // If there are errors in the certificate chain, look at each error to determine the cause.
            if ((sslPolicyErrors & System.Net.Security.SslPolicyErrors.RemoteCertificateChainErrors) != 0)
            {
                if (chain != null && chain.ChainStatus != null)
                {
                    foreach (System.Security.Cryptography.X509Certificates.X509ChainStatus status in chain.ChainStatus)
                    {
                        if ((certificate.Subject == certificate.Issuer) &&
                           (status.Status == System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot))
                        {
                            // Self-signed certificates with an untrusted root are valid. 
                            continue;
                        }
                        else
                        {
                            if (status.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
                            {
                                // If there are any other errors in the certificate chain, the certificate is invalid,
                                // so the method returns false.
                                return false;
                            }
                        }
                    }
                }

                // When processing reaches this line, the only errors in the certificate chain are 
                // untrusted root errors for self-signed certificates. These certificates are valid
                // for default Exchange server installations, so return true.
                return true;
            }
            else
            {
                // In all other cases, return false.
                return false;
            }
        }
    }
}