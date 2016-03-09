using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Gbmono.Api.Admin.Attributes
{
    /// <summary>
    /// ensure the authenticated user has permission to access the role-based reources
    /// if access denied, return status code 403 instead of 401 when user is authenticated
    /// </summary>
    public class RoleBasedAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            // access denied when user is authenticated
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                // return the forbidden status code
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}