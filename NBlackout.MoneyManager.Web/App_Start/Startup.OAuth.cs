using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using NBlackout.Framework.Identity.Services.Mail;
using NBlackout.Framework.Web.Owin.Identity;
using NBlackout.Framework.Web.Owin.Security;
using NBlackout.MoneyManager.Entity;
using Owin;

namespace NBlackout.MoneyManager.Web
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            app.UseOAuthIdentity(new OAuthIdentityOptions
            {
                NameOrConnectionString = "FrameworkContext",
                EmailService = SmtpIdentityMessageServiceFactory.CreateFromAppSettings()
            });
            app.CreatePerOwinContext(() => MoneyManagerDbContext.Create("MoneyManagerDbContext"));

            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                Provider = new OAuthProvider(),
                TokenEndpointPath = new PathString("/token")
            });

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Security/Authentication/SignIn"),
                LogoutPath = new PathString("/Security/Authentication/SignOut"),
                Provider = new IdentityCookieAuthenticationProvider()
            });

            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromDays(7D));
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);
        }
    }
}