using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlogFelipe.Startup))]
namespace BlogFelipe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
