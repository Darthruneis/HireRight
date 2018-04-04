using System;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HireRight
{
    public class MvcApplication : HttpApplication
    {
        private static readonly string LogFileDirectory = Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath("~") + @"\logs");
        private static string LogFilePath => Path.GetFullPath($@"{LogFileDirectory}\{DateTime.UtcNow.Date:dd-MM-yyyy}-log.txt");

        public static void Log(string message)
        {
            if (!Directory.Exists(LogFileDirectory))
                Directory.CreateDirectory(LogFileDirectory);

            //cache the path in case the date changes during this method, and to avoid mapping the path multiple times in one log statement
            var path = LogFilePath;
            if (!File.Exists(path))
                using (var file = File.Create(path))
                    WriteToLogFile(file, message);
            else
                using(var file = File.Open(path, FileMode.Append))
                    WriteToLogFile(file, message);
        }

        private static void WriteToLogFile(Stream stream, string message)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine("--- START - Log event at " + DateTime.Now);
                writer.Write(message);
                writer.WriteLine(Environment.NewLine + "--- END");
            }
        }

        public static void Log(Exception exception)
        {
            if (exception is DbEntityValidationException)
            {
                var validationException = (DbEntityValidationException)exception;
                Log(validationException.EntityValidationErrors.ToString());
                foreach (var errorMessage in validationException.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage))
                {
                    Log(errorMessage);
                }
            }
            Log(exception.ToString());
        }

        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            Log(exception);

            Server.ClearError();
            Response.RedirectToRoute("Error");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}