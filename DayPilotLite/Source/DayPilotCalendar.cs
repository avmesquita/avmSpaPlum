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
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Mvc;
using DayPilot.Web.Mvc.Data;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Enums.Calendar;
using DayPilot.Web.Mvc.Events.Calendar;
using DayPilot.Web.Mvc.Json;
using DayPilot.Web.Mvc.Utils;
using Version = DayPilot.Web.Mvc.Utils.Version;
using ViewType = DayPilot.Web.Mvc.Enums.Calendar.ViewType;

namespace DayPilot.Web.Mvc
{
    /// <summary>
    /// Handles DayPilot Scheduler backend requests (AJAX).
    /// </summary>
    public class DayPilotCalendar 
    {
        private Controller _controller;

        private List<Hashtable> _columns;

        /// <summary>
        /// Gets the update type.
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

        protected Controller Controller { get { return _controller; } }
        private int _days = 1;
        protected int Days
        {
            get
            {
                switch (ViewType)
                {
                    case ViewType.Day:
                        return 1;
                    case ViewType.Week:
                        return 7;
                    case ViewType.WorkWeek:
                        return 5;
                }


                return _days;
            }
            set {
                _days = value < 1 ? 1 : value;
            }
        }

        /// <summary>
        /// Gest or sets the collection of events.
        /// </summary>
        protected IEnumerable Events { get; set; }

        ///<summary>
        /// Gets or sets the start of the business day (in hours).
        ///</summary>
        public int BusinessBeginsHour { get; set; }

        /// <summary>
        /// Gets or sets the end of the business day (in hours).
        /// </summary>
        public int BusinessEndsHour { get; set; }

        /// <summary>
        /// Gets or sets the color of the cells.
        /// </summary>
        public string CellBackColor { get; set; }

        /// <summary>
        /// Gets the id of the control on the client side.
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// Gets or sets the culture that will be used for DateTime formatting.
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets or sets the format of the date display in the header columns (e.g. \"d\", \"yyyy-MM-dd\").
        /// </summary>
        public string HeaderDateFormat { get; set; }

        /// <summary>
        /// Gets or sets the height specification. Determines how the control height will be calculated.
        /// </summary>
        public HeightSpec HeightSpec { get; set; }

        private DateTime _startDate;
        
        /// <summary>
        /// Gets or sets the first day to be displayed in ViewType=Days mode.
        /// </summary>
        public DateTime StartDate
        {
            get
            {
                switch (ViewType)
                {
                    case ViewType.WorkWeek:
                        return Week.FirstWorkingDayOfWeek(_startDate);
                    case ViewType.Week:
                        return Week.FirstDayOfWeek(_startDate, Week.Resolve(WeekStarts));
                }

                return _startDate;
            }
            set { _startDate = value; }
        }

        /// <summary>
        /// Gets or sets the ViewType. ViewType determines how many columns will be displayed (Day, Week, WorkWeek, Days) or whether the Columns collection will be used (Resources).
        /// </summary>
        public ViewType ViewType { get; set; }

        /// <summary>
        /// Gets or sets the time format (12 hour/24 hour clock) to be used for time formatting.
        /// </summary>
        public TimeFormat TimeFormat { get; set; }

        /// <summary>
        /// Gets or sets the visibility of event headers.
        /// </summary>
        public bool EventHeaderVisible { get; set; }

        /// <summary>
        /// Gets or sets the header background color.
        /// </summary>
        public string HourNameBackColor { get; set; }

        /// <summary>
        /// Gets or sets the header font family.
        /// </summary>
        public string HourFontFamily { get; set; }

        /// <summary>
        /// Gets or sets the header font size.
        /// </summary>
        public string HourFontSize { get; set; }

        /// <summary>
        /// Gets or sets the header font color.
        /// </summary>
        public string HourFontColor { get; set; }

        /// <summary>
        /// Gets or sets how the first day of week will be determined (fixed or auto).
        /// </summary>
        public WeekStarts WeekStarts { get; set; }

        /// <summary>
        /// Lifecycle hook. Called at the beginning of every callback request.
        /// </summary>
        protected virtual void OnPrepare() { }

        /// <summary>
        /// Lifecycle hook. Called at the end of every callback request.
        /// </summary>
        protected virtual void OnFinish() { }

        protected virtual void OnCommand(CommandArgs e) { }
        protected virtual void OnEventClick(EventClickArgs e) { }
        protected virtual void OnEventMove(EventMoveArgs e) { }
        protected virtual void OnEventResize(EventResizeArgs e) { }
        protected virtual void OnTimeRangeSelected(TimeRangeSelectedArgs e) { }
        protected virtual void OnInit(InitArgs e) { }

        private string _callbackRedirect;

        ///<summary>
        /// Default constructor.
        ///</summary>
        public DayPilotCalendar()
        {
            UpdateType = CallBackUpdateType.None;
            
            DataTextField = "text";
            DataIdField = "id";
            DataStartField = "start";
            DataEndField = "end";

            Culture = Thread.CurrentThread.CurrentCulture;
            HeaderDateFormat = "d";
            WeekStarts = WeekStarts.Auto;
            CellBackColor = "#FFFFD5";

        }

        /// <summary>
        /// Processes the callback request. This method should be called from the backend controller.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public ActionResult CallBack(Controller c)
        {
            _controller = c;
            return CallBack(c.Request.InputStream);
        }

        public ActionResult CallBack(Stream stream)
        {
            // init
            UpdateType = CallBackUpdateType.None;

            // read request
            JsonData input = SimpleJsonDeserializer.Deserialize(stream);
            var action = (string) input["action"];
            JsonData parameters = input["parameters"];
            JsonData header = input["header"];
            JsonData data = input["data"]; // custom data

            Version.Check((string)header["v"]);

            Id = (string) header["id"];
            StartDate = (DateTime) header["startDate"];
            Days = (int) header["days"];
            HeightSpec = HeightSpecParser.Parse((string) header["heightSpec"]);
            BusinessBeginsHour = (int) header["businessBeginsHour"];
            BusinessEndsHour = (int) header["businessEndsHour"];
            ViewType = ViewTypeParser.Parse((string)header["viewType"]);
            CellBackColor = (string) header["backColor"];
            EventHeaderVisible = header["eventHeaderVisible"] != null ? (bool) header["eventHeaderVisible"] : false;
            TimeFormat = TimeFormatParser.Parse((string) header["timeFormat"]);

            // required for custom hour header rendering
            HourNameBackColor = (string) header["hourNameBackColor"];
            HourFontFamily = (string) header["hourFontFamily"];
            HourFontSize = (string) header["hourFontSize"];
            HourFontColor = (string) header["hourFontColor"];

            OnPrepare();

            switch (action)
            {
                case "Command":
                    OnCommand(CommandArgs.FromJson(parameters, data));
                    break;
                case "EventClick":
                    OnEventClick(EventClickArgs.FromJson(parameters, data));
                    break;
                case "EventMove":
                    OnEventMove(EventMoveArgs.FromJson(parameters, data));
                    break;
                case "EventResize":
                    OnEventResize(EventResizeArgs.FromJson(parameters, data));
                    break;
                case "TimeRangeSelected":
                    OnTimeRangeSelected(TimeRangeSelectedArgs.FromJson(parameters, data));
                    break;
                case "Init":
                    OnInit(new InitArgs());
                    break;
                default:
                    throw new Exception(String.Format("Unknown CallBack command '{0}'.", action));
            }

            OnFinish();

            LoadColumns();

            var result = new Hashtable();

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

            result["Events"] = GetEventsJson();

            if (UpdateType == CallBackUpdateType.Full)
            {
                result["Days"] = Days;
                result["StartDate"] = StartDate.ToString("s");
                result["ViewType"] = ViewType.ToString();
                result["Columns"] = _columns;
            }

            return new JsonResult { Data = result};
        }

        private void LoadColumns()
        {
            _columns = new List<Hashtable>();

            int dayCount = (int)(EndDate - StartDate).TotalDays + 1;

            for (int i = 0; i < dayCount; i++)
            {
                DateTime date = StartDate.AddDays(i);

                Column col = new Column(date.ToString(HeaderDateFormat), null);
                col.Date = date;
                Hashtable c = GetColumn(col);
                _columns.Add(c);

            }
        }

        ///<summary>
        /// Gets the last visible day in ViewType other than Resources.
        ///</summary>
        public DateTime EndDate
        {
            get
            {
                return StartDate.AddDays(Days - 1);
            }
        }

        private Hashtable GetColumn(Column column)
        {
            //BeforeHeaderRenderArgs ea = GetBhrea(column);

            Hashtable c = new Hashtable();

            c["Name"] = column.Name;
            c["Start"] = column.Date.ToString("s");
            c["ToolTip"] = column.ToolTip;
            c["InnerHTML"] = column.Name;

            return c;
        }

        internal TimeSpan Duration()
        {
            return TimeSpan.FromHours(24);
        }

        private List<Hashtable> GetEventsJson()
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

        protected void Update()
        {
            UpdateType = CallBackUpdateType.EventsOnly;
        }

        protected void Update(CallBackUpdateType type)
        {
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