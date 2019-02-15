using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Mvc;
using System.Data;
using DayPilot.Web.Mvc.Json;
using SpaPlum.Web.Service;

namespace SpaPlum.Web.Controllers
{
    public class EventController : SpaPlumBaseController
    {
        public ActionResult Edit(string id)
        {
            var e = new EventManager().Get(id) ?? new EventManager.Event();
            return View(e);
        }

        //[HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            new EventManager().EventEdit(form["Id"], form["Text"]);
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }


        public ActionResult Create()
        {
            return View(new EventManager.Event
            {
                Start = Convert.ToDateTime(Request.QueryString["start"]),
                End = Convert.ToDateTime(Request.QueryString["end"])
            });
        }

//        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Create(FormCollection form)
        {
            DateTime start = Convert.ToDateTime(form["Start"]);
            DateTime end = Convert.ToDateTime(form["End"]);
            new EventManager().EventCreate(start, end, form["Text"]);
            return JavaScript(SimpleJsonSerializer.Serialize("OK"));
        }
    }
}