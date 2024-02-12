using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PassionProject_SuyeonJang_A.Startup))]
namespace PassionProject_SuyeonJang_A
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
