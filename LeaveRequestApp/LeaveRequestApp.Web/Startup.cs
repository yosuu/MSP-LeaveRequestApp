using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeaveRequestApp.Web.Startup))]
namespace LeaveRequestApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
