using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(gradeprogram.Startup))]
namespace gradeprogram
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
