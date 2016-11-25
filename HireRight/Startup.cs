using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HireRight.Startup))]
namespace HireRight
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
