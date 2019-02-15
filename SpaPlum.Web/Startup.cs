using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SpaPlum.Web.Startup))]
namespace SpaPlum.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
