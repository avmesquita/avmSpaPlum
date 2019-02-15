using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SpaPlum.Web.Controllers
{
    public class HomeController : SpaPlumBaseController
    {

		public ActionResult Index()
        {
			return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoginPartial()
        {
            var k = new SpaPlum.Web.Controllers.AccountController();

            ViewBag.NomeDoUsuario = k.HttpContext.Profile;

            return PartialView();
        }

        //[Route("robots.txt", Name = "GetRobotsText"), OutputCache(Duration = 86400)]
        public ContentResult RobotsText()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine("user-agent: *");
            stringBuilder.AppendLine("disallow: /Erro/");
            stringBuilder.AppendLine("allow: /");
            stringBuilder.Append("sitemap: ");
            stringBuilder.AppendLine(this.Url.RouteUrl("GetSitemapXml", "Sitemap", this.Request.Url.Scheme).TrimEnd('/'));

            return this.Content(stringBuilder.ToString(), "text/plain", Encoding.UTF8);
        }

		public ActionResult Planos()
		{
			return View();
		}
    }
}