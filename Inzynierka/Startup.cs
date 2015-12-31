using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inzynierka.Startup))]
namespace Inzynierka
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
