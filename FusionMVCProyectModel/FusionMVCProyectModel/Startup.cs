using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(FusionMVCProyectModel.Startup))]

namespace FusionMVCProyectModel
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(
                new Microsoft.Owin.Security.Cookies.CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                    LoginPath = new PathString("/account/login")
                });

            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            //app.UseMicrosoftAccountAuthentication(clientId: "1234567890123456", clientSecret: "12345678901234567890123456789012");

            //app.UseFacebookAuthentication(
            //    appId: "183404749073681",
            //    appSecret: "12345678901234567890123456789012");



            //var mo = new Microsoft.Owin.Security.MicrosoftAccount.MicrosoftAccountAuthenticationOptions
            //{
            //  ClientId = "1234567890123456",
            //  ClientSecret = "12345678901234567890123456789012"
            //};
            /*            mo.Scope.Add("wl.basic");*/ // Acceso a información básica. mo.Scope.Add("wl.emails");
                                                      // Acceso al correo electrónico app.UseMicrosoftAccountAuthentication(mo); 
        }
    }
}
