using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CBT_APP.Startup))]
namespace CBT_APP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
