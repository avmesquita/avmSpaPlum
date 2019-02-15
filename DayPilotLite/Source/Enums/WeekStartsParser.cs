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
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DayPilot.Web.Mvc.Enums
{
    internal class WeekStartsParser
    {
        internal static WeekStarts Parse(string input)
        {
            switch (input.ToUpper())
            {
                case "MONDAY":
                    return WeekStarts.Monday;
                case "TUESDAY":
                    return WeekStarts.Tuesday;
                case "WEDNESDAY":
                    return WeekStarts.Wednesday;
                case "THURSDAY":
                    return WeekStarts.Thursday;
                case "FRIDAY":
                    return WeekStarts.Friday;
                case "SATURDAY":
                    return WeekStarts.Saturday;
                case "SUNDAY":
                    return WeekStarts.Sunday;
                case "AUTO":
                    return WeekStarts.Auto;
                default:
                    throw new ArgumentException("Unrecognized WeekStarts value.");

            }
        }

        internal static WeekStarts FromIntJavaScript(int input)
        {
            switch (input)
            {
                case 0:
                    return WeekStarts.Sunday;
                case 1:
                    return WeekStarts.Monday;
                case 2:
                    return WeekStarts.Tuesday;
                case 3:
                    return WeekStarts.Wednesday;
                case 4:
                    return WeekStarts.Thursday;
                case 5:
                    return WeekStarts.Friday;
                case 6:
                    return WeekStarts.Saturday;
                default:
                    throw new ArgumentException("Unrecognized WeekStarts int value.");
            }
        }
    }
}
