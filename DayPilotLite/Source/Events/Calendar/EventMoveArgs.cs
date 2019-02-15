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
using DayPilot.Web.Mvc.Data;
using DayPilot.Web.Mvc.Json;

namespace DayPilot.Web.Mvc.Events.Calendar
{
    public class EventMoveArgs
    {
        private EventMoveArgs()
        {
        }

        private EventMoveArgs(JsonData parameters, JsonData data)
        {
            Id = (string)parameters["e"]["value"];
            OldStart = (DateTime)parameters["e"]["start"];
            OldEnd = (DateTime)parameters["e"]["end"];
            Text = (string)parameters["e"]["text"];
            Data = data;

            NewStart = (DateTime) parameters["newStart"];
            NewEnd = (DateTime) parameters["newEnd"];
        }

        ///<summary>
        /// Event value (<see cref="DayPilotCalendar.DataIdField">DayPilotCalendar.DataValueField</see> property).
        ///</summary>
        public string Id { get; private set; }

        ///<summary>
        /// Original event starting date and time (<see cref="DayPilotCalendar.DataStartField">DayPilotCalendar.DataStartField</see> property).
        ///</summary>
        public DateTime OldStart { get; private set; }

        ///<summary>
        /// Original event ending date and time (<see cref="DayPilotCalendar.DataEndField">DayPilotCalendar.DataEndField</see> property).
        ///</summary>
        public DateTime OldEnd { get; private set; }

        ///<summary>
        /// New event starting date and time.
        ///</summary>
        public DateTime NewStart { get; private set; }

        ///<summary>
        /// New event ending date and time.
        ///</summary>
        public DateTime NewEnd { get; private set; }

        /// <summary>
        /// Event text. (<see cref="DayPilotCalendar.DataTextField">DayPilotCalendar.DataTextField</see> property).
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Custom data.
        /// </summary>
        public JsonData Data { get; private set; }

        internal static EventMoveArgs FromJson(JsonData parameters, JsonData data)
        {
            return new EventMoveArgs(parameters, data);
        }
    }
}
