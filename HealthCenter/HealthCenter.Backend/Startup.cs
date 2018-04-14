using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HealthCenter.Backend.Startup))]
namespace HealthCenter.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
