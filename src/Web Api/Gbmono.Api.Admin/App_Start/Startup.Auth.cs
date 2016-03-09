using System;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Gbmono.Api.Admin.Security.Identities;
using Gbmono.Api.Admin.Security;

namespace Gbmono.Api.Admin
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            // Create db context
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            // Create user manager
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            // Role manager
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            //// Enable the application to use a cookie to store information for the signed in user, in our case, it's removable. 
            //app.UseCookieAuthentication(new CookieAuthenticationOptions());
            //// and to use a cookie to temporarily store information about a user logging in with a third party login provider, in our case, it's removable. 
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";
            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId),
                // AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),  // for third party login, removable.
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true
            };

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);
        }
    }
}