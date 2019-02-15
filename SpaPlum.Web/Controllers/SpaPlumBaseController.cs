using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SpaPlum.Web.Controllers
{
    [HandleError(ExceptionType = typeof(HttpAntiForgeryException),
             View = "~/Views/Erro/NoCookieSupport")]
    [HandleError(View = "~/Views/Erro/Index")]
    public class SpaPlumBaseController : Controller
    {
        /*
        protected override void Initialize(RequestContext requestContext)
        {
            string cultureInfo = requestContext.RouteData.GetRequiredString("pt-BR");
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo(cultureInfo);
            System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(cultureInfo);
            base.Initialize(requestContext);
        }
        */

        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

    }
}