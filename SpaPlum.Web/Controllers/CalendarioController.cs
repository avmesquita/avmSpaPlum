using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DayPilot.Web.Mvc;
using DayPilot.Web.Mvc.Enums;
using DayPilot.Web.Mvc.Events.Calendar;
using SpaPlum.Web.Service;
using System.Data;
using SpaPlum.Web.Entity;

namespace SpaPlum.Web.Controllers
{
    public class CalendarioController : Controller
    {

        #region DayPilotCalendar
        //
        // GET: /Backend/

        public ActionResult Backend()
        {
            return new Dpc().CallBack(this);
        }


        class Dpc : DayPilotCalendar
        {
            protected override void OnInit(InitArgs e)
            {
                Update();
            }

            protected override void OnEventResize(EventResizeArgs e)
            {
                new EventManager().EventMove(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnEventMove(EventMoveArgs e)
            {
                new EventManager().EventMove(e.Id, e.NewStart, e.NewEnd);
                Update();
            }

            protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
            {
                new EventManager().EventCreate(e.Start, e.End, "New event");
                Update();
            }

            protected override void OnFinish()
            {
                if (UpdateType == CallBackUpdateType.None)
                {
                    return;
                }

                Events = new EventManager().FilteredData(StartDate, StartDate.AddDays(Days)).AsEnumerable();

                DataIdField = "CodigoAgendamento";
                DataTextField = "NomeCliente";
                DataStartField = "DataInicial";
                DataEndField = "DataFinal";
            }

        }
        #endregion


        #region FullCalendar

        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public string Init()
        {
            bool rslt = false; // Utils.InitialiseDiary();
            return rslt.ToString();
        }

        [Authorize]
        public void UpdateEvent(int id, string NewEventStart, string NewEventEnd)
        {
            new AgendamentoEvent().UpdateDiaryEvent(id, NewEventStart, NewEventEnd);
        }

        [Authorize]
        public bool SaveEvent(string Title, string NewEventDate, string NewEventTime, string NewEventDuration)
        {
            //return new AgendamentoEvent().CreateNewEvent(Title, NewEventDate, NewEventTime, NewEventDuration);
            return true;
        }

        [Authorize]
        public bool GerarAgendamento(int codigoFilial, int codigoServico, int codigoStatusAgendamento, int codigoTerapeuta,
                                      string nomeCliente, string emailCliente, string observacao, int qtdePeriodos, int tipoMassagem,
                                   decimal valor, decimal valorEmpresa, decimal valorTerapeuta, decimal valorLiquido, decimal valorTaxaAdicional,
                                   decimal valorTaxaPagto, decimal valorMoraPagto, string DataInicio, string DataFim)
        {
            return new AgendamentoEvent().CreateNewEvent(codigoFilial, codigoServico, codigoStatusAgendamento, codigoTerapeuta,
                                                         nomeCliente, emailCliente, observacao, qtdePeriodos, tipoMassagem,
                                                         valor, valorEmpresa, valorTerapeuta, valorLiquido, valorTaxaAdicional,
                                                         valorTaxaPagto, valorMoraPagto, DataInicio, DataFim);
        }


        [Authorize]
        public JsonResult GetDiarySummary(double start, double end)
        {
            //var ApptListForDate = new AgendamentoEvent().LoadAppointmentSummaryInDateRange(start, end);

            var ApptListForDate = new AgendamentoEvent().LoadAppointmentDetailInDateRange(start, end);
            
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult GetDiaryEvents(double start, double end)
        {
            var ApptListForDate = new AgendamentoEvent().GetAllAgendamentosInDateRange(start, end);
            var eventList = from e in ApptListForDate
                            select new
                            {
                                id = e.ID,
                                title = e.Title,
                                start = e.StartDateString,
                                end = e.EndDateString,
                                color = e.StatusColor,
                                className = e.ClassName,
                                someKey = e.SomeImportantKeyID,
                                allDay = false
                            };
            var rows = eventList.ToArray();
            return Json(rows, JsonRequestBehavior.AllowGet);
        }

        #endregion


    }
}