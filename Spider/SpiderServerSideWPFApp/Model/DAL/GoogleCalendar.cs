using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
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
        static string ApplicationName = "Calendar API Quickstart";
        

        public static string GetEvents()
        {
            UserCredential credential = Login();

            return GetData(credential);
        }

        private static string GetData(UserCredential credential)
        {
            // Create Calendar Service.
            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            Console.WriteLine("Upcoming events:");
            Events events = request.Execute();

            string stringWithEvents = "";
            if (events.Items.Count > 0)
            {
                foreach (var eventItem in events.Items)
                {
                    string when = eventItem.Start.DateTime.ToString();
                    if (String.IsNullOrEmpty(when))
                    {
                        when = eventItem.Start.DateTime.ToString();
                    }
                    stringWithEvents += "{0} ({1})" + eventItem.Summary + eventItem.Start.DateTime.ToString();
                }
            }
            else
            {
                return "No upcoming events found.";
            }
            return stringWithEvents;
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