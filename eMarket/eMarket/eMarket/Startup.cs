using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eMarket.Startup))]
namespace eMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
