using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HubFintech.Startup))]
namespace HubFintech
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
