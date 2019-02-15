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
using System.Globalization;
using System.Text;

namespace DayPilot.Web.Mvc.Utils
{
    public class Locale
    {
        public static string RegistrationString(string culture)
        {
            DateTimeFormatInfo fi = new CultureInfo(culture, false).DateTimeFormat;
            StringBuilder sb = new StringBuilder();

            sb.Append("DayPilot.Locale.register(new DayPilot.Locale('");
            sb.Append(culture);
            sb.Append("', ");

            sb.Append("{");

            sb.Append("'dayNames':['");
            sb.Append(String.Join("','", fi.DayNames));
            sb.Append("']");
            sb.Append(",");

            sb.Append("'dayNamesShort':['");
            sb.Append(String.Join("','", fi.ShortestDayNames));
            sb.Append("']");
            sb.Append(",");

            sb.Append("'monthNames':['");
            sb.Append(String.Join("','", fi.MonthNames));
            sb.Append("']");
            sb.Append(",");

            sb.Append("'monthNamesShort':['");
            sb.Append(String.Join("','", fi.AbbreviatedMonthNames));
            sb.Append("']");
            sb.Append(",");

            sb.Append("'timePattern':'");
            sb.Append(fi.ShortTimePattern);
            sb.Append("'");
            sb.Append(",");

            sb.Append("'datePattern':'");
            sb.Append(fi.ShortDatePattern);
            sb.Append("'");
            sb.Append(",");

            sb.Append("'dateTimePattern':'");
            sb.Append(fi.ShortDatePattern);
            sb.Append(" ");
            sb.Append(fi.ShortTimePattern);
            sb.Append("'");
            sb.Append(",");

            sb.Append("'timeFormat':'");
            sb.Append(fi.ShortTimePattern.Contains("tt") ? "Clock12Hours" : "Clock24Hours");
            sb.Append("'");
            sb.Append(",");

            sb.Append("'weekStarts':");
            sb.Append((int)fi.FirstDayOfWeek);
            sb.Append("");

            sb.Append("}");

            sb.Append("));");

            return sb.ToString();

        }
    }
}
