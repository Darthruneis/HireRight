using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace HireRight
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Error()
        {
            var exception = Server.GetLastError();

            using (StreamWriter writer = new StreamWriter(File.Open("~/log.txt", FileMode.OpenOrCreate)))
            {
                writer.WriteLine("--- START");
                writer.WriteLine("Exception logged at: " + DateTime.Now);
                writer.Write(exception);
                writer.WriteLine("--- END");
            }
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