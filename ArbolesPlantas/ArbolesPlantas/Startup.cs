using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArbolesPlantas.Startup))]
namespace ArbolesPlantas
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
