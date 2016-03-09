using Microsoft.Owin;
using Owin;


namespace Gbmono.Api.Admin
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}