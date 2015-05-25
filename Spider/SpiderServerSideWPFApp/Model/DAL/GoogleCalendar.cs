using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using SpiderServerSideWPFApp.Model.BLL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SpiderServerSideWPFApp.Model.DAL
{
    public class GoogleCalendar
    {
        static string[] Scopes = { CalendarService.Scope.CalendarReadonly };
        static string ApplicationName = "Varmeregleringen i kyrkan";


        public static CalendarEvent[] GetEvents()
        {
            UserCredential credential = Login();

            return GetData(credential);
        }

        private static CalendarEvent[] GetData(UserCredential credential)
        {
            // Create Calendar Service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            var calendarID = "kalender@missionskyrkorna.se";
            //var calendarID = "primary";
            EventsResource.ListRequest request = service.Events.List(calendarID);
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            // New list for events
            List<CalendarEvent> EventsList = new List<CalendarEvent>(10);

            // Process the events
            Console.WriteLine("Upcoming events:");
            Events events = request.Execute();
            if (events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string summary = eventItem.Summary;
                    string when = eventItem.Start.DateTime.ToString();
                    string location = eventItem.Location;
                    DateTime start;
                    DateTime end;
                    if (String.IsNullOrEmpty(when))
                    {
                        DateTime.TryParse(eventItem.Start.Date.ToString(), out start);
                        DateTime.TryParse(eventItem.Start.Date.ToString(), out end);
                        end = end.AddHours(23);
                        end = end.AddMinutes(59);
                        end = end.AddSeconds(59);
                    }
                    else
                    {
                        DateTime.TryParse(eventItem.Start.DateTime.ToString(), out start);
                        DateTime.TryParse(eventItem.End.DateTime.ToString(), out end);
                    }
                    //Console.WriteLine("{0}, {1} ({2})", eventItem.Summary, where, when);

                    CalendarEvent ev = new CalendarEvent(summary, location, start, end);
                    EventsList.Add(ev);
                    Console.WriteLine("{0}, {1} ({2} - {3})", ev.Summary, ev.Location, ev.Start, ev.End);
                }
            }
            else
            {
                Console.WriteLine("No upcoming events found.");
            }

            EventsList.TrimExcess();

            // Array to return
            CalendarEvent[] EventsArray = EventsList.ToArray();

            return EventsArray;
        }

        static UserCredential Login()
        {
            UserCredential credential;

            using (var stream = new FileStream(@"Components\client_secret.json", FileMode.Open,
                FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(System.Environment
                  .SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                   GoogleClientSecrets.Load(stream).Secrets,
                  Scopes,
                  "user",
                  CancellationToken.None,
                  new FileDataStore(credPath, true)).Result;
            }

            return credential;
        }
    }
}