using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;
using Gbmono.Api.Models;
using Gbmono.Api.Security.Identities;
using Gbmono.Api.HttpResults;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Accounts")]
    public class AccountsController : ApiController
    {        
        private readonly RepositoryManager _repoManager;
        private GbmonoUserManager _userManager;

        public AccountsController()
        {
            _repoManager = new RepositoryManager();
        }

        public GbmonoUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<GbmonoUserManager>(); }
        }

        [Route("Current")]
        public async Task<GbmonoUser> GetUser()
        {
            // return current authorized user
            return await UserManager.FindByNameAsync(User.Identity.Name);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("Register")]
        public async Task<IHttpActionResult> Create([FromBody]UserBindingModel model)
        {
            // check if user name already exists
            var existedUser = UserManager.FindByName(model.UserName);

            if(existedUser != null)
            {
                return new DataInvalidResult(string.Format("{0} 已经存在.", model.UserName), Request);
            }

            var displayName = model.UserName.Split('@')[0];
            // we use email as username in gbmoni user db 
            var user = new GbmonoUser() { UserName = model.UserName, Email = model.Email, CreateTime = DateTime.Now, DisplayName = displayName };
            IdentityResult result = await UserManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return new DataInvalidResult("注册失败.", Request);
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
