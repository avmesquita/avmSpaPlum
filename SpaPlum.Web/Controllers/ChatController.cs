using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaPlum.Web.Models;
using SpaPlum.Web.Entity;
using SpaPlum.Web.Contexto;
using System.Data.Entity.Validation;

namespace SpaPlum.Web.Controllers
{
    public class ChatController : Controller
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        // ajax query - add message
        public JsonResult AddMessage(NewMessageViewModel message)
        {
            var result = new { message = "" };

            try
            {
                Mensagem newMessage = new Mensagem();
                newMessage.Username = message.Username.Substring(0, message.Username.IndexOf('@')).ToString();
                if (newMessage.Username.Length > 20) // Limitação do campo
                {
                    newMessage.Username = newMessage.Username.Substring(0, 19);
                }
                newMessage.CorpoMensagem = message.MessageBody;
                newMessage.DataPost = DateTime.Now;

                db.MensagemModels.Add(newMessage);

                db.SaveChanges();

                result = new { message = "Success" };
            }
            catch (DbEntityValidationException e)
            {
                string msg = "";
                foreach (var eve in e.EntityValidationErrors)
                {
                    msg += string.Format("Entidade de tipo \"{0}\" no estado \"{1}\" possui o(s) seguinte(s) erro(s):",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);

                    foreach (var ve in eve.ValidationErrors)
                    {
                        msg += string.Format("- Propriedade: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                //throw new Exception("Falha na verificação de dados." + Environment.NewLine + msg);
                result = new { message = "Opa! Ocorreu não previsto na validação. [" + msg + "]" };
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // ajax query - get top ten message sorted by date desc
        [Authorize]
        [NoCache]
        [HandleError]
        public JsonResult GetMessages()
        {
            DateTime hoje = DateTime.Now.Date;
            var messages = db.MensagemModels.Where(x => x.DataPost > hoje).OrderByDescending(x => x.DataPost).Take(10);
            var result = new LinkedList<object>();
            foreach (var message in messages.ToList())
            {
                result.AddLast(new { Username = message.Username, PostDateTime = message.DataPost.ToString(), MessageBody = message.CorpoMensagem });
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

    }
}