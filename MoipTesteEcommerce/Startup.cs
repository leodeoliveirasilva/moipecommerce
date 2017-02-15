using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MoipTesteEcommerce.Startup))]
namespace MoipTesteEcommerce
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
