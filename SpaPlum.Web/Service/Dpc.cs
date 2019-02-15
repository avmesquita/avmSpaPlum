using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using SpaPlum.Web.Contexto;
using SpaPlum.Web.Entity;

namespace SpaPlum.Web.Service
{
    public class Dpc : DayPilotCalendar
    {
        private AgendamentoContexto db = new AgendamentoContexto();

        protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
        {
            string name = (string)e.Data["name"];
            if (String.IsNullOrEmpty(name))
            {
                name = "(default)";
            }
            new EventManager().EventCreate(e.Start, e.End, name);
            Update();
        }

        protected override void OnEventMove(DayPilot.Web.Mvc.Events.Calendar.EventMoveArgs e)
        {
            if (new EventManager().Get(e.Id) != null)
            {
                new EventManager().EventMove(e.Id, e.NewStart, e.NewEnd);
            }
            
            Agendamento agendamentoModel = db.AgendamentoModels.Find(e.Id);
            agendamentoModel.DataInicial = e.NewStart.ToLocalTime();
            agendamentoModel.DataFinal = e.NewEnd.ToLocalTime();
            db.Entry(agendamentoModel).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            Update();
        }

        protected override void OnEventClick(EventClickArgs e)
        {

        }

        protected override void OnEventResize(DayPilot.Web.Mvc.Events.Calendar.EventResizeArgs e)
        {
            new EventManager().EventMove(e.Id, e.NewStart, e.NewEnd);
            Update();
        }

        protected override void OnCommand(CommandArgs e)
        {
            switch (e.Command)
            {
                case "navigate":
                    StartDate = (DateTime)e.Data["start"];
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
            //Events = new EventManager().Data.AsEnumerable();

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