using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Testing.BLL.Interfaces;
using Testing.BLL.Services;

[assembly: OwinStartup(typeof(Testing.WEB.App_Start.Startup))]
[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)]
namespace Testing.WEB.App_Start
{
    public class Startup
    {
        IServiceCreator serviceCreator = new ServiceCreator();
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<IUserService>(CreateUserService);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private IUserService CreateUserService()
        {
           // return serviceCreator.CreateUserService("DefaultConnection");

            IUserService userService = serviceCreator.CreateUserService("DefaultConnection");
            userService.EmailService = new EmailService();
            userService.SetDefaultTokenProvider();
            return userService;

        }
    }
}