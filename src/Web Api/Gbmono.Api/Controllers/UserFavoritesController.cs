using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Models;
using Gbmono.Api.Extensions;
using Gbmono.Api.Security.Identities;

namespace Gbmono.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/UserFavorites")]
    public class UserFavoritesController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;
        private GbmonoUserManager _userManager;
        public GbmonoUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<GbmonoUserManager>(); }
        }

        // ctor
        public UserFavoritesController()
        {
            _repositoryManager = new RepositoryManager();

        }

        // get user favorit products
        [Route("Products/{pageIndex:int?}/{pageSize:int?}")]
        public async Task<IEnumerable<ProductSimpleModel>> GetFavorites(int? pageIndex = 1, int? pageSize = 10)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            // start index 
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;

            // load user favorite products
            var productIds = await _repositoryManager.UserFavoriteRepository
                                                   .Table
                                                   .Where(m => m.UserId == user.Id)
                                                   .OrderByDescending(m => m.Created)
                                                   .Select(m => m.ProductId)
                                                   .ToListAsync();

            var products = await _repositoryManager.ProductRepository
                                                    .Table
                                                    .Include(m => m.Brand)                                                    
                                                    .Include(m => m.Images) // include product images
                                                    .Where(m => productIds.Contains(m.ProductId))
                                                    .ToListAsync();
            // return simplified models
            return products.Select(m => m.ToSimpleModel()).Skip(startIndex).Take(pageSize.Value);

        }

        // get user favorite
        [AllowAnonymous]
        [Route("IsFavorited/{productId}")]
        public async Task<bool> GetByProduct(int productId)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // user is not authenticated
                return false;
            }

            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            var favorite = await _repositoryManager.UserFavoriteRepository
                                                   .Table
                                                   .SingleOrDefaultAsync(m => m.UserId == user.Id &&
                                                                              m.ProductId == productId);

            return favorite != null;                                           
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Favorite favorite)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            // check if it already exists
            var userFavorite = await _repositoryManager.UserFavoriteRepository
                                                       .Table
                                                       .SingleOrDefaultAsync(m => m.ProductId == favorite.ProductId &&
                                                                                  m.UserId == user.Id);

            if (userFavorite == null)
            {
                // create 
                _repositoryManager.UserFavoriteRepository.Create(new UserFavorite
                {
                    UserId = user.Id,
                    ProductId = favorite.ProductId,
                    Created = DateTime.Now
                });

                await _repositoryManager.UserFavoriteRepository.SaveAsync();
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{productId}")]
        public async Task<IHttpActionResult> Delete(int productId)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            var entityToDelete = await _repositoryManager.UserFavoriteRepository
                                                   .Table
                                                   .SingleOrDefaultAsync(m => m.UserId == user.Id &&
                                                                              m.ProductId == productId);

            // delete 
            _repositoryManager.UserFavoriteRepository.Delete(entityToDelete);
            await _repositoryManager.UserFavoriteRepository.SaveAsync();

            return Ok();
        }
    }
}
