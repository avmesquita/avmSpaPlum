using SpaPlum.Web.Contexto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaPlum.Web.Controllers
{
    public class PixelsController : SpaPlumBaseController
    {
        private string _imagepath = "~/Content/images/pixel.png";
        private string _imagetype = "image/png";

        private AgendamentoContexto db = new AgendamentoContexto();

        // GET: Pixels
        public ActionResult Index()
        {
            return base.File(_imagepath, _imagetype);
        }

        public ActionResult GetLink(string id)
        {
            ViewBag.LinkImage = string.Format("<img src='https://central.corpuspa.com.br/Pixels/EmailDeAgendamento/{0}'>", id);

            return View();
        }

        public ActionResult EmailDeAgendamento(string id)
        {
            Int64 idInteiro = 0;
            if (id != null)
            {
                Int64.TryParse(id, out idInteiro);
            }

            if (idInteiro > 0)
            {
                try
                {
                    var email = db.EmailModels.Where(x => x.CodigoAgendamento == idInteiro).FirstOrDefault();
                    email.FoiLido = true;
                    db.Entry(email).State = EntityState.Modified;
                    db.SaveChangesAsync();
                }
                catch { }
            }

            return base.File(_imagepath, _imagetype);
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult GetFile()
        {
            // No need to dispose the stream, MVC does it for you
            string path = _imagepath;
            FileStream stream = new FileStream(path, FileMode.Open);
            FileStreamResult result = new FileStreamResult(stream, _imagetype);
            result.FileDownloadName = "pixel.png";
            return result;
        }

    }
}