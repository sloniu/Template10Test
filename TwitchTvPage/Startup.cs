using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitchTvPage.Startup))]
namespace TwitchTvPage
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
