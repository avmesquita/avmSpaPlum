/*
Copyright © 2005 - 2016 Annpoint, s.r.o.

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
using System.Drawing;
using System.Reflection;
using System.Text;
using DayPilot.Web.Mvc.Json;

namespace DayPilot.Web.Mvc.Init
{
    internal class InitBuilder
    {
        private readonly StringBuilder sb = new StringBuilder();
        private const string TempVar = "v";

        private string id;

        internal void AppendSerialized(string property, object value)
        {
            AppendProp(property, SimpleJsonSerializer.Serialize(value), false);
        }

        internal void AppendProp(string property, object val, bool apo)
        {
            if (apo)
            {
                string escapedVal = null;
                if (val != null)
                {
                    escapedVal = val.ToString().Replace("\'", "\\\'");
                }
                sb.AppendLine(TempVar + "." + property + " = '" + escapedVal + "';");
            }
            else
            {
                sb.AppendLine(TempVar + "." + property + " = " + val + ";");
            }
        }

        internal void AppendProp(string property, object val)
        {
            if (val == null)
            {
                AppendProp(property, "null", false);
            }
            else
            {
                AppendProp(property, val, true);
            }
        }

        internal void AppendProp(string property, bool value)
        {
            AppendProp(property, value.ToString().ToLower(), false);
        }

        internal void AppendProp(string property, int value)
        {
            AppendProp(property, value, false);
        }

        internal void AppendProp(string property, double value)
        {
            AppendProp(property, value, false);
        }

        internal void Open(string className, string id)
        {
            this.id = id;

            sb.AppendFormat("<div id='{0}'></div>", id); sb.AppendLine();
            sb.AppendLine("<script type='text/javascript'>");
            sb.AppendLine(String.Format("/* DayPilot Lite for ASP.NET MVC: {0} */", Assembly.GetExecutingAssembly().GetName().Version));
            sb.AppendFormat("var v = new {0}('{1}');", className, id); sb.AppendLine();
        }

        internal void Close()
        {
            sb.AppendLine("v.Init();");
            sb.AppendFormat("var {0} = v;", id); sb.AppendLine();
            sb.AppendLine("</script>");
        }

        public override string ToString()
        {
            return sb.ToString();
        }

        internal void InjectString(string text)
        {
            sb.AppendLine(text);
        }
    }
}
