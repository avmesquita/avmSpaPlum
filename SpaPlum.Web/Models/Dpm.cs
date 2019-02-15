using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Events.Month;
using SpaPlum.Web.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
/*
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Data;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Enums.Calendar;
using DayPilot.Web.Mvc.Events.Calendar;
using DayPilot.Web.Mvc.Events.Month;
using DayPilot.Web.Mvc.Json;
using DayPilot.Web.Mvc.Utils;
*/
namespace SpaPlum.Web.Models
{
    public class Dpm : DayPilotMonth
    {
        protected override void OnInit(InitArgs initArgs)
        {
            // this select is a really bad example, no where clause
            Events = new EventManager().FilteredData(VisibleStart, VisibleEnd).AsEnumerable();

            DataStartField = "eventstart";
            DataEndField = "eventend";
            DataTextField = "name";
            DataIdField = "id";

            Update();
        }
    }
}