using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Project.Genius.Web.Startup))]
namespace Project.Genius.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
