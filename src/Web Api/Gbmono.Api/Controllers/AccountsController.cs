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

        [Authorize]
        [Route("Favorites")]
        public async Task<IEnumerable<UserFavorite>> GetFavorites()
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            // load user favorites
            var userFavorites = await _repoManager.UserFavoriteRepository
                                                  .Table
                                                  .Where(m => m.UserId == user.Id)
                                                  .ToListAsync();

            return userFavorites;
        }

        [Authorize]
        [HttpPost]
        [Route("AddFavorite")]
        public async Task<IHttpActionResult> AddFavorite([FromBody] Favorite favorite)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            // check if it already exists
            var userFavorite = await _repoManager.UserFavoriteRepository
                                                 .Table
                                                 .SingleOrDefaultAsync(m => m.ProductId == favorite.ProductId &&
                                                                            m.UserId == user.Id);

            if(userFavorite == null)
            {
                // create 
                _repoManager.UserFavoriteRepository.Create(new UserFavorite
                {
                    UserId = user.Id,
                    ProductId = favorite.ProductId,
                    Created = DateTime.Now
                });

                await _repoManager.UserFavoriteRepository.SaveAsync();
            }

            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("RemoveFavorite")]
        public async Task<IHttpActionResult> RemoveFavorite([FromBody] Favorite favorite)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            // check if it already exists
            var model = await _repoManager.UserFavoriteRepository
                                             .Table
                                             .SingleOrDefaultAsync(m => m.ProductId == favorite.ProductId &&
                                                                        m.UserId == user.Id);

            if (model != null)
            {
                // delete 
                _repoManager.UserFavoriteRepository.Delete(model);
                await _repoManager.UserFavoriteRepository.SaveAsync();
            }

            return Ok();
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
