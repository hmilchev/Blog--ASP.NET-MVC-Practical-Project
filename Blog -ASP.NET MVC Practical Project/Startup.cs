using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Blog__ASP.NET_MVC_Practical_Project.Startup))]
namespace Blog__ASP.NET_MVC_Practical_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
