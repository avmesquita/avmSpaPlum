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
using System.Data;
using System.Linq;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Data;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using DayPilot.Web.Mvc.Json;

namespace MvcApplication1.Controllers
{
    [HandleError]
    public class CalendarController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Light()
        {
            return RedirectToAction("ThemeTransparent");
        }

        public ActionResult Red()
        {
            return RedirectToAction("Index");
        }

        public ActionResult Green()
        {
            return RedirectToAction("ThemeGreen");
        }

        public ActionResult ThemeGreen()
        {
            return View();
        }

        public ActionResult ThemeWhite()
        {
            return View();
        }

        public ActionResult ThemeTransparent()
        {
            return View();
        }

        public ActionResult ThemeGoogleLike()
        {
            return View();
        }

        public ActionResult ThemeTraditional()
        {
            return View();
        }

        public ActionResult ThemeBlue()
        {
            return View();
        }

        public ActionResult NextPrevious()
        {
            return View();
        }
        public ActionResult JQuery()
        {
            return View();
        }

        public ActionResult EventCreating()
        {
            return View();
        }

        public ActionResult EventMoving()
        {
            return View();
        }

        public ActionResult EventResizing()
        {
            return View();
        }

        public ActionResult NoEventHeader()
        {
            return RedirectToAction("Index");
        }

        public ActionResult CellHeight()
        {
            return View();
        }
        
        public ActionResult Hours24()
        {
            return View();
        }

        public ActionResult Day()
        {
            return RedirectToActionPermanent("ViewDay");
        }
        
        public ActionResult WorkWeek()
        {
            return RedirectToActionPermanent("ViewWorkWeek");
        }

        public ActionResult Week()
        {
            return RedirectToActionPermanent("ViewWeek");
        }

        public ActionResult ViewDay()
        {
            return View();
        }
        
        public ActionResult ViewWorkWeek()
        {
            return View();
        }

        public ActionResult ViewWeek()
        {
            return View();
        }

        public ActionResult ViewResources()
        {
            return View();
        }

        public ActionResult ViewDaysResources()
        {
            return View();
        }

        public ActionResult Rtl()
        {
            return View();
        }

        public ActionResult EventsAllDay()
        {
            return View();
        }

        public ActionResult EventActiveAreas()
        {
            return View();
        }

        public ActionResult EventArrangement()
        {
            return View();
        }

        public ActionResult EventContextMenu()
        {
            return View();
        }

        public ActionResult EventSelecting()
        {
            return View();
        }

        public ActionResult ExternalDragDrop()
        {
            return View();
        }

        public ActionResult RecurringEvents()
        {
            return View();
        }

        public ActionResult Crosshair()
        {
            return View();
        }

        public ActionResult Today()
        {
            return View();
        }

        public ActionResult FixedColumnWidth()
        {
            return View();
        }

        public ActionResult AutoRefresh()
        {
            return View();
        }

        public ActionResult Notify()
        {
            return View();
        }

        public ActionResult HeaderAutoFit()
        {
            return View();
        }

        public ActionResult TimeHeaderCellDuration()
        {
            return View();
        }

        public ActionResult DayRange()
        {
            return View();
        }

        public ActionResult AutoHide()
        {
            return View();
        }

        public ActionResult Message()
        {
            return View();
        }

        public ActionResult Height100Pct()
        {
            return View();
        }



        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }

        public class Dpc : DayPilotCalendar
        {
            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                string name = (string)e.Data["name"];
                if (String.IsNullOrEmpty(name))
                {
                    name = "(default)";
                }
                new EventManager(Controller).EventCreate(e.Start, e.End, name);
                Update();
            }

            protected override void OnEventMove(DayPilot.Web.Mvc.Events.Calendar.EventMoveArgs e)
            {
                if (new EventManager(Controller).Get(e.Id) != null)
                {
                    new EventManager(Controller).EventMove(e.Id, e.NewStart, e.NewEnd);
                }

                Update();
            }

            protected override void OnEventClick(EventClickArgs e)
            {
                
            }

            protected override void OnEventResize(DayPilot.Web.Mvc.Events.Calendar.EventResizeArgs e)
            {
                new EventManager(Controller).EventMove(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnCommand(CommandArgs e)
            {
                switch (e.Command)
                {
                    case "navigate":
                        StartDate = (DateTime) e.Data["start"];
                        Update(CallBackUpdateType.Full);
                        break;

                    case "refresh":
                        Update();
                        break;

                    case "previous":
                        StartDate = StartDate.AddDays(-7);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "next":
                        StartDate = StartDate.AddDays(7);
                        Update(CallBackUpdateType.Full);
                        break;

                    case "today":
                        StartDate = DateTime.Today;
                        Update(CallBackUpdateType.Full);
                        break;

                }
            }

            protected override void OnInit(InitArgs initArgs)
            {
                Update(CallBackUpdateType.Full);
            }

            protected override void OnFinish()
            {
                // only load the data if an update was requested by an Update() call
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                // this select is a really bad example, no where clause
                Events = new EventManager(Controller).Data.AsEnumerable();

                /*
                foreach (DataRow dr in (EnumerableRowCollection) Events)
                {
                    dr["text"] = "custom HTML";
                }
                 */

                DataStartField = "start";
                DataEndField = "end";
                DataTextField = "text";
                DataIdField = "id";
            }

        }

    }
}
