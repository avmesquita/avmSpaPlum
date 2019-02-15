using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaPlum.Web.Controllers
{
    public class ErroController : Controller
    {
        // GET: Erro
        [AllowAnonymous]
        public ActionResult Index()
        {
			switch (Response.StatusCode)
			{
				case 400: return View("BadRequest");
				case 401: return View("NotAuthorized");
				case 403: return View("Forbidden");
				case 404: return View("NotFound");
				case 405: return View("TimeOut");
				case 450: return View("WindowsParentControl");
				case 500: return View("InternalError");
				case 503: return View("Maintenance");
				case 601: return View("PagamentoIndisponivel");
				default: return View("Index");
			}            
        }

        [AllowAnonymous]
        public ViewResult NotFound()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        [AllowAnonymous]
        public ViewResult InvalidRequest()
        {
            Response.StatusCode = 400;
            //return View("InvalidRequest");
            return View("BadRequest");
        }

        [AllowAnonymous]
        public ViewResult NotAuthorized()
        {
            Response.StatusCode = 401;
            return View("NotAuthorized");
        }

        [AllowAnonymous]
        public ViewResult Forbidden()
        {
            Response.StatusCode = 403;
            return View("Forbidden");
        }

        [AllowAnonymous]
        public ViewResult TimedOut()
        {
            Response.StatusCode = 405;
            return View("TimedOut");
        }

        [AllowAnonymous]
        public ViewResult WindowsParentControl()
        {
            Response.StatusCode = 450;
            return View("WindowsParentControl");
        }

        [AllowAnonymous]
        public ViewResult InternalError()
        {
            Response.StatusCode = 500;
            return View("InternalError");
        }

        [AllowAnonymous]
        public ViewResult Maintenance()
        {
            Response.StatusCode = 503;
            return View("Maintenance");
        }

        [AllowAnonymous]
        public ViewResult PagamentoIndisponivel()
        {
            Response.StatusCode = 601;
            return View("PagamentoIndisponivel");
        }



    }
}