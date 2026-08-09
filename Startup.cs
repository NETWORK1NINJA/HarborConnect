using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HarborConnect.Startup))]
namespace HarborConnect
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
