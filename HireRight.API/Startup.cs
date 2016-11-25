using HireRight.API;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace HireRight.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}