using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HandymanTools.Startup))]
namespace HandymanTools
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
