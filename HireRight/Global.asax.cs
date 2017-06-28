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
        private static readonly string LogFilePath = Path.GetFullPath(System.Web.HttpContext.Current.Server.MapPath("~") + @"\logs.txt");

        public static void Log(string message)
        {
            using (StreamWriter writer = new StreamWriter(File.Open(LogFilePath, FileMode.Append)))
            {
                writer.WriteLine("--- START");
                writer.WriteLine("Log event at " + DateTime.Now);
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
            Response.RedirectToRoute("Default");
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (!File.Exists(LogFilePath))
                File.Create(LogFilePath);
        }
    }
}