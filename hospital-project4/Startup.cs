using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hospital_project4.Startup))]
namespace hospital_project4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
