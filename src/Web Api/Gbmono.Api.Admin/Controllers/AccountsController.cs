using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;

using Gbmono.Api.Admin.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Admin.Security.Identities;
using Microsoft.AspNet.Identity;


namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/Accounts")]
    public class AccountsController : ApiController
    {
        private readonly RepositoryManager _repoManager;
       

        public AccountsController()
        {
            _repoManager = new RepositoryManager();
        }

        #region user manager & role manager
        private ApplicationUserManager _userManager = null;
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? (_userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>() ); }
        }

        private ApplicationRoleManager _roleManager = null;
        public ApplicationRoleManager RoleManager
        {
            get { return _roleManager ?? (_roleManager = Request.GetOwinContext().Get<ApplicationRoleManager>()); }
        }
        #endregion

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Create([FromBody]UserBindingModel model)
        {
            // we use email as username in gbmoni user db 
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };

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
