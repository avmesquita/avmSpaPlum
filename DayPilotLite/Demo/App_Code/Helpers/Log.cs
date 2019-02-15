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
using System.Runtime.CompilerServices;
using System.Web;

/// <summary>
/// Summary description for Log
/// </summary>
public class Log
{
    private static HttpApplication _app = null;
    private static HttpServerUtility Server
    {
        get
        {
            if (HttpContext.Current != null)
                return HttpContext.Current.Server;

            if (_app == null)
                _app = new HttpApplication();
            return _app.Server;
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static void Debug(string msg)
    {

        string dir = HttpContext.Current.Server.MapPath("~/App_Data/Log/");
        Directory.CreateDirectory(dir);
        using (StreamWriter sw = File.AppendText(dir + "log.txt"))
        {
            sw.WriteLine(DateTime.Now + " " + msg);
        }

    }

}
