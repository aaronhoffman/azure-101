using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NetAtNoonAzure101.Startup))]
namespace NetAtNoonAzure101
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
