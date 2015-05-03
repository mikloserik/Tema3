using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tema2.Startup))]
namespace Tema2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
