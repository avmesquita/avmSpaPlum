using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SpaPlum.Entity
{
	public class iCalendar
	{
		public DateTime EventStartDateTime { get; set; }
		public DateTime EventEndDateTime { get; set; }
		public DateTime EventTimeStamp { get; set; }
		public DateTime EventCreatedDateTime { get; set; }
		public DateTime EventLastModifiedTimeStamp { get; set; }
		public string UID { get; set; }
		public string EventDescription { get; set; }
		public string EventLocation { get; set; }
		public string EventSummary { get; set; }
		public string AlarmTrigger { get; set; }
		public string AlarmRepeat { get; set; }
		public string AlarmDuration { get; set; }
		public string AlarmDescription { get; set; }

		public iCalendar()
		{
			EventTimeStamp = DateTime.Now;
			EventCreatedDateTime = EventTimeStamp;
			EventLastModifiedTimeStamp = EventTimeStamp;
		}

		public static Char[] iCalsConvert(List<iCalendar> iCals)
		{
			string calendars = null;

			foreach (iCalendar iCal in iCals)
			{
				System.Text.StringBuilder sb = new System.Text.StringBuilder();

				//Calendar
				sb.AppendLine("BEGIN:VCALENDAR");
				sb.AppendLine("PRODID:-//AVM Sistemas//SpaPlum.Web//EN");
				sb.AppendLine("VERSION:2.0");
				sb.AppendLine("METHOD:REQUEST");

				//Event
				sb.AppendLine("BEGIN:VEVENT");
				sb.AppendLine("DTSTART:" + toUniversalTime(iCal.EventStartDateTime));
				sb.AppendLine("DTEND:" + toUniversalTime(iCal.EventEndDateTime));
				sb.AppendLine("DTSTAMP:" + toUniversalTime(iCal.EventTimeStamp));
				sb.AppendLine("ORGANIZER;CN=Corpus Spa:mailto:agendamento@corpuspa.com.br");
				sb.AppendLine("UID:" + iCal.UID);
				//sb.AppendLine("ATTENDEE;CUTYPE=INDIVIDUAL;ROLE=REQ-PARTICIPANT;PARTSTAT=NEEDS-ACTION;RSVP=TRUE;CN=marydoe@company.com;X-NUM-GUESTS=0:mailto:marydoe@company.com");
				sb.AppendLine("CREATED:" + toUniversalTime(iCal.EventCreatedDateTime));
				sb.AppendLine("X-ALT-DESC;FMTTYPE=text/html:" + iCal.EventDescription);
				sb.AppendLine("LAST-MODIFIED:" + toUniversalTime(iCal.EventLastModifiedTimeStamp));
				sb.AppendLine("LOCATION:" + iCal.EventLocation);
				sb.AppendLine("SEQUENCE:0");
				sb.AppendLine("STATUS:CONFIRMED");
				sb.AppendLine("SUMMARY:" + iCal.EventSummary);
				sb.AppendLine("TRANSP:OPAQUE");

				//Alarm
				sb.AppendLine("BEGIN:VALARM");
				sb.AppendLine("TRIGGER:" + String.Format("-PT{0}M", iCal.AlarmTrigger));
				sb.AppendLine("REPEAT:" + iCal.AlarmRepeat);
				sb.AppendLine("DURATION:" + String.Format("PT{0}M", iCal.AlarmDuration));
				sb.AppendLine("ACTION:DISPLAY");
				sb.AppendLine("DESCRIPTION:" + iCal.AlarmDescription);
				sb.AppendLine("END:VALARM");

				sb.AppendLine("END:VEVENT");
				sb.AppendLine("END:VCALENDAR");

				calendars += sb.ToString();
			}

			return calendars.ToCharArray();
		}

		public static string toUniversalTime(DateTime dt)
		{
			string DateFormat = "yyyyMMddTHHmmssZ";

			return dt.ToUniversalTime().ToString(DateFormat);
		}

		public System.Net.Mail.Attachment MailAttachmentAgendamento(List<iCalendar> iCals)
		{

			var calendars = iCalsConvert(iCals);

			foreach (var iCal in calendars)
			{
				var calendarBytes = System.Text.Encoding.UTF8.GetBytes(calendars);
				System.IO.MemoryStream ms = new System.IO.MemoryStream(calendarBytes);

				return new System.Net.Mail.Attachment(ms, String.Format("agendamentoCorpusSpa.ics"), "text/calendar");								
			}
			return null;
			//List<iCalendar> iCals = new List<iCalendar>();
/*
			foreach (var res in reg.Reservations)
			{
				iCals.Add(new iCalendar
				{
					EventStartDateTime = Convert.ToDateTime(res.BeginDate),
					EventEndDateTime = Convert.ToDateTime(res.EndDate),
					UID = Guid.NewGuid().ToString(),
					EventDescription = res.DetailsHTML,
					EventLocation = res.Location,
					EventSummary = res.Summary,
					AlarmTrigger = "30",
					AlarmRepeat = "2",
					AlarmDuration = "15",
					AlarmDescription = res.Details
				});
			}
			*/
			//List<string> calendars = Common.iCals(iCals);


		}

	}
}