using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SpaPlum.GoogleCalendar
{
    public class Servico
    {
        // If modifying these scopes, delete your previously saved credentials
        // at ~/.credentials/calendar-dotnet-quickstart.json
        string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        string ApplicationName = "Google Calendar API .NET Quickstart";

        public CalendarService servico = null;

        public bool conectarGoogleCalendar()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/calendar-dotnet-quickstart.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;

                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Calendar API service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            this.servico = service;

            if (this.servico != null)
                return true;
            else
                return false;
        }


        public Events listarApontamentos(CalendarService service)
        {
            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // List events.
            Events events = request.Execute();
            Console.WriteLine("Próximos eventos:");
            if (events.Items != null && events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.Date;
                    }
                    Console.WriteLine("{0} ({1})", eventItem.Summary, when);
                }
            }
            else
            {
                Console.WriteLine("Não há compromissos próximos.");
            }
            Console.Read();

            return events;
        }


        void incluirApontamento(CalendarService service)
        {
            if (service != null)
            {
                Event myEvent = new Event
                {
                    Summary = "Appointment",
                    Location = "Somewhere",
                    Start = new EventDateTime()
                    {
                        DateTime = new DateTime(2014, 6, 2, 10, 0, 0),
                        TimeZone = "America/Sao_Paulo"
                    },
                    End = new EventDateTime()
                    {
                        DateTime = new DateTime(2014, 6, 2, 10, 30, 0),
                        TimeZone = "America/Sao_Paulo"
					},
                    Recurrence = new String[] { "RRULE:FREQ=WEEKLY;BYDAY=MO" },
                    Attendees = new List<EventAttendee>()
                    {
                        new EventAttendee() { Email = "servicos@avmsistemas.net" }
                    }
                };

                Event recurringEvent = service.Events.Insert(myEvent, "primary").Execute();
            }

        }
    }
}
