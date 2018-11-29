using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SaguaroFinal.Startup))]
namespace SaguaroFinal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
