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
using System.Threading;
using System.Web.Mvc;
using System.Collections;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Month;
using DayPilot.Web.Mvc.Json;
using DayPilot.Web.Mvc.Utils;
using Version = DayPilot.Web.Mvc.Utils.Version;

namespace DayPilot.Web.Mvc
{
    /// <summary>
    /// Handles DayPilot Scheduler backend requests (AJAX).
    /// </summary>
    public class DayPilotMonth 
    {

        /// <summary>
        /// Gets or sets the month (ViewType=Month) or first week (ViewType=Weeks) to be displayed.
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// First day of week.
        /// </summary>
        public WeekStarts WeekStarts { get; set; }

        /// <summary>
        /// Header background color. Ignored in CssOnly mode.
        /// </summary>
        public string HeaderBackColor { get; set; }

        /// <summary>
        /// Business cell background color. Ignored in CssOnly mode.
        /// </summary>
        public string BackColor { get; set; }

        /// <summary>
        /// Non-business cell background color. Ignored in CssOnly mode.
        /// </summary>
        public string NonBusinessBackColor { get; set; }

        /// <summary>
        /// Id of the control on the client side.
        /// </summary>
        public string Id { get; private set; }


        /// <summary>
        /// Requested update type (None, EventsOnly, Full).
        /// </summary>
        public CallBackUpdateType UpdateType { get; private set; }


        /// <summary>
        /// Gets or sets the name of an event member that holds the event text.
        /// </summary>
        protected string DataTextField { get; set; }

        /// <summary>
        /// Gets or sets the name of an event member that holds the event id.
        /// </summary>
        protected string DataIdField { get; set; }

        /// <summary>
        /// Gets or sets the name of an event member that holds the event start.
        /// </summary>
        protected string DataStartField { get; set; }

        /// <summary>
        /// Gets or sets the name of an event member that holds the event end.
        /// </summary>
        protected string DataEndField { get; set; }


        private IEnumerable _events;
        private object _callbackData;

        protected Controller Controller { get; private set; }

        protected DayPilotMonth()
        {
            DataEndField = "end";
            DataStartField = "start";
            DataIdField = "id";
            DataTextField = "text";

            WeekStarts = WeekStarts.Auto;
            UpdateType = CallBackUpdateType.None;
        }

        /// <summary>
        /// Lifecycle hook. Called at the beginning of every request.
        /// </summary>
        protected virtual void OnPrepare() { }
        protected virtual void OnFinish() { }

        protected virtual void OnInit(InitArgs e) { }
        protected virtual void OnCommand(CommandArgs e) { }

        protected virtual void OnEventClick(EventClickArgs e) { }
        protected virtual void OnEventMove(EventMoveArgs e) { }
        protected virtual void OnEventResize(EventResizeArgs e) { }

        protected virtual void OnTimeRangeSelected(TimeRangeSelectedArgs e) { }


        private string _callbackRedirect;

        public ActionResult CallBack(Controller c)
        {
            Controller = c;

            // init
            UpdateType = CallBackUpdateType.None;

            // read request
            JsonData input = SimpleJsonDeserializer.Deserialize(c.Request.InputStream);
            String action = (string) input["action"];
			JsonData parameters = input["parameters"];
			JsonData header = input["header"];
            JsonData data = input["data"]; // custom data

            Version.Check((string)header["v"]);

			StartDate = (DateTime) header["startDate"];
            WeekStarts = WeekStartsParser.FromIntJavaScript((int)header["weekStarts"]);
            HeaderBackColor = (string) header["headerBackColor"];
            BackColor = (string) header["backColor"];
            NonBusinessBackColor = (string) header["nonBusinessBackColor"];
            Id = (string)header["id"];

            OnPrepare();

            switch (action)
            {
                case "EventMove":
                    OnEventMove(EventMoveArgs.FromJson(parameters, new string[] {}, data));
                    break;
                case "EventResize":
                    OnEventResize(EventResizeArgs.FromJson(parameters, new string[] {}, data)); 
                    break;
                case "EventClick":
                    OnEventClick(EventClickArgs.FromJson(parameters, new string[] {}, data));
                    break;
                case "TimeRangeSelected":
                    OnTimeRangeSelected(TimeRangeSelectedArgs.FromJson(parameters, data));
                    break;
                case "Command":
                    OnCommand(CommandArgs.FromJson(parameters, data));
                    break;
                case "Init":
                    OnInit(new InitArgs());
                    break;
            }

            OnFinish();

            Hashtable result = new Hashtable();

            result["UpdateType"] = UpdateType.ToString();

            if (_callbackRedirect != null)
            {
                result["CallBackRedirect"] = _callbackRedirect;
                return new JsonResult { Data = result };
            }

            if (UpdateType == CallBackUpdateType.None)
            {
                return new JsonResult { Data = result };
            }

            result["Events"] = GetEvents();
            result["CallBackData"] = _callbackData;

            if (UpdateType == CallBackUpdateType.Full)
            {
                result["StartDate"] = StartDate.ToString("s");
                result["WeekStarts"] = Week.ResolveAsInt(WeekStarts);
                result["HeaderBackColor"] = HeaderBackColor;
                result["BackColor"] = BackColor;
                result["NonBusinessBackColor"] = NonBusinessBackColor;
            }

            JsonResult json = new JsonResult();
            json.Data = result;

            return json;
        }

        protected IEnumerable Events
        {
            get { return _events; }
            set { _events = value; }
        }

        internal List<Hashtable> GetEvents()
        {
            var result = new List<Hashtable>();

            if (Events == null)
            {
                return result;
            }

            foreach (object dataItem in Events)
            {
                DateTime start = PropertyLoader.GetDateTime(dataItem, DataStartField, "DataStartField");
                DateTime end = PropertyLoader.GetDateTime(dataItem, DataEndField, "DataEndField");
                string name = PropertyLoader.GetString(dataItem, DataTextField, "DataTextField");
                string val = PropertyLoader.GetString(dataItem, DataIdField, "DataIdField");

                var e = new Hashtable();

                e["start"] = start.ToString("s");
                e["end"] = end.ToString("s");
                e["id"] = val;
                e["text"] = name;

                result.Add(e);
            }
            return result;
        }


        public DateTime VisibleStart
        {
            get
            {
                return FirstDay;
            }
        }

        public DateTime VisibleEnd
        {
            get
            {
                 return VisibleStart.AddDays(GetRowCount() * 7);
            }
        }

        internal int GetRowCount()
        {
            DateTime lastVisibleDayOfMonth = LastVisibleDayOfMonth();
            int count = (int)Math.Floor((lastVisibleDayOfMonth - FirstDay).TotalDays) + 1;
            int rowCount = (int)Math.Ceiling((count / 7.0));
            return rowCount;

        }

        /// <summary>
        /// End of the the last visible day of the selected month
        /// </summary>
        /// <returns></returns>
        private DateTime LastVisibleDayOfMonth()
        {
            DateTime last = FirstDayOfMonth.AddMonths(1).AddDays(-1);
            return last;
        }

         private bool IsWeekend(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                return true;
            }
            if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                return true;
            }
            return false;
        }
        public DayOfWeek ResolvedWeekStart
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
        internal DateTime FirstDayOfMonth
        {
            get
            {
                return new DateTime(StartDate.Year, StartDate.Month, 1);
            }
        }

        internal DateTime FirstDay
        {
            get
            {
                DateTime firstWeek = FirstDayOfMonth;
                DateTime first = GetFirstDayOfWeek(firstWeek);

                return first;
            }
        }

        private DateTime GetFirstDayOfWeek(DateTime day)
        {
            switch (WeekStarts)
            {
                case WeekStarts.Sunday:
                    return Week.FirstDayOfWeek(day, DayOfWeek.Sunday);
                case WeekStarts.Monday:
                    return Week.FirstDayOfWeek(day, DayOfWeek.Monday);
                case WeekStarts.Tuesday:
                    return Week.FirstDayOfWeek(day, DayOfWeek.Tuesday);
                case WeekStarts.Wednesday:
                    return Week.FirstDayOfWeek(day, DayOfWeek.Wednesday);
                case WeekStarts.Thursday:
                    return Week.FirstDayOfWeek(day, DayOfWeek.Thursday);
                case WeekStarts.Friday:
                    return Week.FirstDayOfWeek(day, DayOfWeek.Friday);
                case WeekStarts.Saturday:
                    return Week.FirstDayOfWeek(day, DayOfWeek.Saturday);

                case WeekStarts.Auto:
                    return Week.FirstDayOfWeek(day);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected void Update()
        {
            UpdateType = CallBackUpdateType.EventsOnly;
        }

        protected void Update(CallBackUpdateType type)
        {
            UpdateType = type;
        }

        protected void Update(object data)
        {
            _callbackData = data;
            UpdateType = CallBackUpdateType.EventsOnly;
        }

        protected void Update(object data, CallBackUpdateType type)
        {
            _callbackData = data;
            UpdateType = type;
        }

        /// <summary>
        /// Redirects the page to a new location specified by the url parameter.
        /// </summary>
        /// <param name="url">The new location</param>
        protected void Redirect(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException("Please specify the URL.");
            }
            var urlHelper = new UrlHelper(Controller.Request.RequestContext);
            _callbackRedirect = urlHelper.Content(url);
        }

    }
}
