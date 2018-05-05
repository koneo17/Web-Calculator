using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(DeveloperEvaluation.Startup))]

namespace DeveloperEvaluation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
