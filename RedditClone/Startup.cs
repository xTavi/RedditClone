using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RedditClone.Startup))]
namespace RedditClone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
