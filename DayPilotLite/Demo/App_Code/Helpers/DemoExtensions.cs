/*
Copyright © 2005 - 2014 Annpoint, s.r.o.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.

-------------------------------------------------------------------------

NOTE: Reuse requires the following acknowledgement (see also NOTICE):
This product includes DayPilot (http://www.daypilot.org) developed by Annpoint, s.r.o.
*/

using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Json;
using System.Web.Hosting;

namespace App_Code.Helpers
{
    public static class DemoExtensions
    {
        public static IHtmlString GetDownloadLink(this HtmlHelper helper)
        {
            return new HtmlString(String.Format("<a href='{0}'>{1}</a>", GetDownloadUrl(helper), GetDownloadName(helper)));
        }

        public static string GetBuild(this HtmlHelper helper)
        {
            return Assembly.GetAssembly(typeof(DayPilotCalendar)).GetName().Version.ToString();
        }		

        public static string GetDownloadName(this HtmlHelper helper)
        {
            Version v = Assembly.GetAssembly(typeof(DayPilotCalendar)).GetName().Version;
            return String.Format("DayPilotLiteMvc-{0}.{1}.{2}.zip", v.Major, v.Minor, v.Build);
        }
		
        public static string GetDownloadUrl(this HtmlHelper helper)
        {
            bool isSandbox = helper.ViewContext.HttpContext.Request.Path.ToLower().Contains("sandbox");
            bool isDemo = helper.ViewContext.HttpContext.Request.Path.ToLower().Contains("demo");

            if (isSandbox)
            {
                return Resolve(helper, String.Format("~/{0}", GetDownloadName(helper)));
            }
            if (isDemo)
            {
                return String.Format("/files/{0}", GetDownloadName(helper));
            }
            return GetDownloadName(helper);
        }

        private static string Resolve(HtmlHelper helper, string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                return url;
            }
            var urlHelper = new UrlHelper(helper.ViewContext.RequestContext);
            return urlHelper.Content(url);            
        }


        public static IHtmlString Menu(this HtmlHelper helper)
        {
            string action = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();
            string controller = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();

            if (controller == "Home")
            {
                return new HtmlString(String.Empty);
            }

            StringBuilder sb = new StringBuilder();
            string config = HostingEnvironment.MapPath("~/Views/" + controller + "/Navigation.json");

            JsonData data = SimpleJsonDeserializer.Deserialize(File.ReadAllText(config));

            for (int i = 0; i < data.Count; i++)
            {
                JsonData tab = data[i];

                String url = (string)tab["url"];
                String title = (string)tab["title"];
                bool pro = tab["pro"] != null && (bool)tab["pro"];

                if (String.IsNullOrEmpty(url))
                {
                    sb.Append("<div class='header'>");
                    sb.Append(title);
                    sb.Append("</div>");
                }
                else
                {
                    sb.Append("<div><a href='");

                    UrlHelper u = new UrlHelper(helper.ViewContext.RequestContext);
                    string printUrl = u.Action(url, controller, null);
                    sb.Append(printUrl);
                    if (url == "Index")
                    {
                        sb.Append("/");
                    }
                    sb.Append("'");

                    if (action == url)
                    {
                        sb.Append(" class='selected'");
                    }

                    sb.Append("><span>");
                    sb.Append(title);
                    //sb.Append(" a:" + action);
                    sb.Append("</span>");
                    if (pro)
                    {
                        sb.Append(" <span class='pro'>PRO</span>");
                    }
                    sb.Append("</a></div>");
                }
            }
            return new HtmlString(sb.ToString());

        }

        public static IHtmlString Tabs(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
            HttpRequestBase request = helper.ViewContext.HttpContext.Request;

            string action = helper.ViewContext.Controller.ValueProvider.GetValue("action").RawValue.ToString();
            string controller = helper.ViewContext.Controller.ValueProvider.GetValue("controller").RawValue.ToString();

            string config = request.MapPath("~/Demo.config");

            sb.Append("<div>");
            JsonData data = SimpleJsonDeserializer.Deserialize(File.ReadAllText(config));

            string description = String.Empty;
            for (int i = 0; i < data.Count; i++)
            {
                JsonData tab = data[i];

                String url = (string)tab["url"];
                String title = (string) tab["title"];

                sb.Append("<a class='");
			    if (controller == url) {
				    description = (string) tab["description"];
				    sb.Append("tab selected");
			    }
			    else {
				    sb.Append("tab");
			    }

                UrlHelper u = new UrlHelper(helper.ViewContext.RequestContext);
                string printUrl = u.Action("Index", url, null);

                if (!printUrl.EndsWith("/"))
                {
                    printUrl += "/";
                }

			    sb.Append("' href='");
			    sb.Append(printUrl);
			    sb.Append("'><span style='width: 100px; text-align:center;'>");
			    sb.Append(title);
                sb.Append("</span>");
                sb.Append("</a>");
            }

            // closing

            sb.Append("</div>");

            sb.Append("<div class='header'><div class='bg-help'>");
		    sb.Append(description);
		    sb.Append("</div></div>");


            return new HtmlString(sb.ToString());
        }
    }
}
