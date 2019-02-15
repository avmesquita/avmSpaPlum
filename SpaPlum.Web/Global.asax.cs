using SpaPlum.Web.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Helpers;
using System.Web.SessionState;

namespace SpaPlum.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["UsersOnline"] = 0;

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ModelBinders.Binders.Add(typeof(decimal?), new DecimalModelBinder());

            AntiForgeryConfig.SuppressXFrameOptionsHeader = true;
        }

        protected void Application_Error()
        {
            var registroDeLogs = new RegistroDeLogsService(Server.MapPath("~/SpaPlumLogs"));

            registroDeLogs.RegistrarLog(Server.GetLastError());
        }

        public void Session_OnStart()
        {
            Application.Lock();
            Application["UsersOnline"] = (int)Application["UsersOnline"] + 1;
            Application.UnLock();
        }

        public void Session_OnEnd()
        {
            Application.Lock();
            Application["UsersOnline"] = (int)Application["UsersOnline"] - 1;
            Application.UnLock();
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            //this.EndRequest += MvcApplication_EndRequest;
            base.Init();
        }

        void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
        }

        void MvcApplication_EndRequest(object sender, EventArgs e)
        {
            var request = this.Request;
            var response = this.Response;

            if (request.RawUrl.Contains("Content/"))
            {
                response.Cache.SetExpires(DateTime.Now.AddMinutes(7200));
                response.Cache.SetLastModified(DateTime.Now);
                response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
                //request.Headers.Add("Content-Type", "application/x-gzip");
                //request.Headers.Add("Content-Encoding", "gzip");
            }
        }

    }
}
