using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SpaPlum.Web.Controllers
{
    public class SitemapController : Controller
    {
        // GET: Sitemap
        //[Route("sitemap.xml", Name ="GetSitemapXml"),OutputCache(Duration = 86400)]
        public ActionResult Index()
        {
            //return this.Content(xmlString, "text/xml");

            return View();
        }

        
    }
}