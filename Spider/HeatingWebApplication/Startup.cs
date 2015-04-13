using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HeatingWebApplication.Startup))]
namespace HeatingWebApplication
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
