using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CustomCaptcha.Startup))]
namespace CustomCaptcha
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
