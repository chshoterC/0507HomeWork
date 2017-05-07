using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_0507HomeWrok.Startup))]
namespace _0507HomeWrok
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
