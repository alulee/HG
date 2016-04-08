using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HGenealogy.Startup))]
namespace HGenealogy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
