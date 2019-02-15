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
using System.Threading;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Month;

namespace DayPilot.Web.Mvc
{
    public class DayPilotMonthConfig
    {

        public string BackColor { get; set; }
        public string BackendUrl { get; set; }
        public string BorderColor { get; set; }
        public string CellHeaderBackColor { get; set; }
        public string CellHeaderFontColor { get; set; }
        public string CellHeaderFontFamily { get; set; }
        public string CellHeaderFontSize { get; set; }
        public int CellHeaderHeight { get; set; }
        public int CellHeight { get; set; }
        public bool CssOnly { get; set; }
        public string EventBackColor { get; set; }
        public string EventBorderColor { get; set; }
        public string EventFontColor { get; set; }
        public string EventFontFamily { get; set; }
        public string EventFontSize { get; set; }
        public int EventHeight { get; set; }
        public string InnerBorderColor { get; set; }
        public string HeaderBackColor { get; set; }
        public string HeaderFontColor { get; set; }
        public string HeaderFontFamily { get; set; }
        public string HeaderFontSize { get; set; }
        public int HeaderHeight { get; set; }
        public bool HideUntilInit { get; set; }
        public string NonBusinessBackColor { get; set; }
        public bool ShowToolTip { get; set; }
        public string Theme { get; set; }
        public TimeFormat TimeFormat { get; set; }
        public WeekStarts WeekStarts { get; set; }
        public string Width { get; set; }
        public DateTime StartDate { get; set; }


        public EventClickHandlingType EventClickHandling { get; set; }
        public EventMoveHandlingType EventMoveHandling { get; set; }
        public EventResizeHandlingType EventResizeHandling { get; set; }
        public TimeRangeSelectedHandlingType TimeRangeSelectedHandling { get; set; }

        public string EventClickJavaScript { get; set; }
        public string EventDoubleClickJavaScript { get; set; }
        public string EventSelectJavaScript { get; set; }
        public string EventRightClickJavaScript { get; set; }
        public string EventMoveJavaScript { get; set; }
        public string EventResizeJavaScript { get; set; }
        public string TimeRangeSelectedJavaScript { get; set; }
        public string TimeRangeDoubleClickJavaScript { get; set; }
        public string HeaderClickJavaScript { get; set; }

        public DayPilotMonthConfig()  {
            EventClickHandling = EventClickHandlingType.Disabled;
            EventMoveHandling = EventMoveHandlingType.Disabled;
            EventResizeHandling = EventResizeHandlingType.Disabled;
            TimeRangeSelectedHandling = TimeRangeSelectedHandlingType.Disabled;

            BackColor = "#ffffff";
            BorderColor = "#CED2CE";
            CellHeaderBackColor = "#ffffff";
            CellHeaderFontColor = "#42658C";
            CellHeaderFontFamily = "Tahoma, Arial, Helvetica, sans-serif";
            CellHeaderFontSize = "10pt";
            CellHeight = 100;
            CellHeaderHeight = 16;
            CssOnly = true;
            EventBackColor = "#2951A5";
            EventBorderColor = "#2951A5";
            EventFontColor = "#ffffff";
            EventFontFamily = "Tahoma, Arial, Helvetica, sans-serif";
            EventFontSize = "8pt";
            EventHeight = 25;
            InnerBorderColor = "#CCCCCC";
            HeaderBackColor = "#F3F3F9";
            HeaderFontColor = "#42658C";
            HeaderFontFamily = "Tahoma, Arial, Helvetica, sans-serif";
            HeaderFontSize = "10pt";
            HeaderHeight = 20;
            NonBusinessBackColor = "#ffffff";
            ShowToolTip = true;
            TimeFormat = TimeFormat.Auto;
            WeekStarts = WeekStarts.Auto;
            Width = "100%";
            StartDate = DateTime.Today;

        }

        internal int WeekStartInt
        {
            get
            {
                switch (ResolvedWeekStart)
                {
                    case DayOfWeek.Sunday:
                        return 0;
                    case DayOfWeek.Monday:
                        return 1;
                    case DayOfWeek.Tuesday:
                        return 2;
                    case DayOfWeek.Wednesday:
                        return 3;
                    case DayOfWeek.Thursday:
                        return 4;
                    case DayOfWeek.Friday:
                        return 5;
                    case DayOfWeek.Saturday:
                        return 6;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Day of week specified using WeekStarts property, translated to an actual DayOfWeek.
        /// </summary>
        internal DayOfWeek ResolvedWeekStart
        {
            get
            {
                switch (WeekStarts)
                {
                    case WeekStarts.Sunday:
                        return DayOfWeek.Sunday;
                    case WeekStarts.Monday:
                        return DayOfWeek.Monday;
                    case WeekStarts.Tuesday:
                        return DayOfWeek.Tuesday;
                    case WeekStarts.Wednesday:
                        return DayOfWeek.Wednesday;
                    case WeekStarts.Thursday:
                        return DayOfWeek.Thursday;
                    case WeekStarts.Friday:
                        return DayOfWeek.Friday;
                    case WeekStarts.Saturday:
                        return DayOfWeek.Saturday;
                    case WeekStarts.Auto:
                        return Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }



    }
}
