using SpaPlum.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SpaPlum.Web.Service;

namespace SpaPlum.Web.Controllers
{
	public class FaleConoscoController : SpaPlumBaseController
	{
		// GET: FaleConosco
		[Authorize]
		public ActionResult Index()
		{
			if (  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
            	  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
	              (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");

			return RedirectToAction("Create");
		}

		[Authorize]
		public ActionResult Create()
		{
			if ((!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("0")) ||
	  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("1")) ||
	  (!SpaPlum.Web.Models.UserExtendedModel.GetCodigoPerfil(User).Equals("4")))
				RedirectToAction("NotAuthorized", "Erro");


			return View();
		}

		[HttpPost]
		public ActionResult Create(FaleConoscoModel model)
		{
			try
			{
				new Mailer().EnviarEmail(model);

				return RedirectToAction("Index", "Home");
			}
			catch
			{
				return View();
			}
		}
	}
}