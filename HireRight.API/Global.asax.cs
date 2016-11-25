using HireRight.EntityFramework.CodeFirst.Database_Context;
using Ninject;
using Ninject.Modules;
using System.Data.Entity;
using System.Web;
using System.Web.Http;

namespace HireRight.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            HireRightDbContext context = new HireRightDbContext();
            context.Database.Initialize(false);
        }
    }
}