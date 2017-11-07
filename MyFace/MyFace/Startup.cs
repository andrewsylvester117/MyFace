using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyFace.Startup))]
namespace MyFace
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
