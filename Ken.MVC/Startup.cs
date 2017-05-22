using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ken.MVC.Startup))]
namespace Ken.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
