using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;

using Gbmono.Api.Security.Identities;

namespace Gbmono.Api.Security
{
    public class ApplicationOAuthProvider: OAuthAuthorizationServerProvider
    {
        //  The "client_id" parameter for the current request. The Authorization Server application
        //  is responsible for validating this value identifies a registered client.
        private readonly string _publicClientId;

        // ctor
        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        // Called when a request to the Token endpoint arrives with a "grant_type" of "password".
        // This occurs when the user has provided name and password credentials directly
        // into the client application's user interface, and the client application is using
        // those to acquire an "access_token" and optional "refresh_token". 
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var userManager = context.OwinContext.GetUserManager<GbmonoUserManager>();

            // lookup user by user name and password
            GbmonoUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            // create user identity for Bearer token
            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager, OAuthDefaults.AuthenticationType);

            // create user identity for cookie
            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager, CookieAuthenticationDefaults.AuthenticationType);

            // create properties, user name or other extra information
            AuthenticationProperties properties = CreateProperties(user);

            // initialize a new instance of the Microsoft.Owin.Security.AuthenticationTicket
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);

            // call the context.Validated(ticket) to tell the OAuth server to protect the ticket as an access token and send it out in JSON payload.
            // to issue an access token the context.Validated must be called with a new ticket containing the claims about the resource owner
            // which should be associated with the access token.
            context.Validated(ticket);

            // Signs the cookie identity so it can send the authentication cookie.
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        // Called at the final stage of a successful Token endpoint request. An application
        // may implement this call in order to do any final modification of the claims being
        // used to issue access or refresh tokens. This call may also be used in order to
        // add additional response parameters to the Token endpoint's json response body.
        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        //  Called to validate that the origin of the request is a registered "client_id",
        //  and that the correct credentials for that client are present on the request.
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        //  Called to validate that the context.ClientId is a registered "client_id", and
        //  that the context.RedirectUri a "redirect_uri" registered for that client. This
        //  only occurs when processing the Authorize endpoint. The application MUST implement
        //  this call, and it MUST validate both of those factors before calling context.Validated.
        //  If the context.Validated method is called with a given redirectUri parameter,
        //  then IsValidated will only become true if the incoming redirect URI matches the
        //  given redirect URI. If context.Validated is not called the request will not proceed
        //  further.
        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        /// <summary>
        /// return user name, display name and other user related info 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static AuthenticationProperties CreateProperties(GbmonoUser user)
        {
            // extract the user profile name from user name (email)
            var userDisplayName = user.DisplayName;

            // if profile display name exists in db
    
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", user.UserName },
                { "displayName", userDisplayName }
            };
            return new AuthenticationProperties(data);
        }
    }
}