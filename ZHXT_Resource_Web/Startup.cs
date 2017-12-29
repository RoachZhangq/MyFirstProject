using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZHXT_Resource_Web.Startup))]
namespace ZHXT_Resource_Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
           // ConfigureAuth(app);
        }
    }
}
