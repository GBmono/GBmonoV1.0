using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Gbmono.Api.Models;
using Gbmono.Api.Security.Identities;
using Newtonsoft.Json.Linq;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Accounts")]
    public class AccountsController : ApiController
    {
        private readonly GbmonoUserManager _userManager = null;

        public GbmonoUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<GbmonoUserManager>(); }
        }

        /// <summary>
        /// to check if current token is valid
        /// it returns 401 if token is expired or invalid
        /// </summary>
        /// <returns></returns>
        [Route("Current")]
        [Authorize]
        public CurrentUser GetCurrentUser()
        {
            // get the user object by current user name
            var gbmonoUser = UserManager.FindByName(User.Identity.Name);

            // reset password and other sensitive info before returning user object
            return new CurrentUser
            {
                UserName = gbmonoUser.UserName,
                DisplayName = gbmonoUser.DisplayName,
                Email = gbmonoUser.Email
            };
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Create([FromBody]UserBindingModel model)
        {
            var displayName = model.UserName.Split('@')[0];
            var user = new GbmonoUser() { UserName = model.UserName, Email = model.Email, CreateTime = DateTime.Now, DisplayName = displayName };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            return Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("message", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
