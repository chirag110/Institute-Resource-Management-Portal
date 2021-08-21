using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmptyProject.Startup))]
namespace EmptyProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
