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

        // get user favorite products
        [Route("Products/{pageIndex:int?}/{pageSize:int?}")]
        public async Task<IEnumerable<ProductSimpleModel>> GetSavedProducts(int? pageIndex = 1, int? pageSize = 10)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            // start index 
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;

            // load user saved product ids
            var productIds = await _repositoryManager.UserProductRepository
                                                     .Table
                                                     .Where(m => m.UserId == user.Id)
                                                     .OrderByDescending(m => m.Created)
                                                     .Select(m => m.ProductId)
                                                     .ToListAsync();

            var products = await _repositoryManager.ProductRepository
                                                    .Table
                                                    .Include(m => m.Brand)
                                                    .Include(m => m.Images) // include product images
                                                    .Where(m => productIds.Contains(m.ProductId) &&
                                                                m.IsPublished == true)
                                                    .OrderBy(m => m.PrimaryName)
                                                    .Skip(startIndex)
                                                    .Take(pageSize.Value)
                                                    .ToListAsync();
            // return simplified models
            return products.Select(m => m.ToSimpleModel());

        }

        // get user favorite articles
        [Route("Articles/{pageIndex:int?}/{pageSize:int?}")]
        public async Task<IEnumerable<ArticleSimpleModel>> GetSavedArticles(int? pageIndex = 1, int? pageSize = 10)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            // start index 
            var startIndex = (pageIndex.Value - 1) * pageSize.Value;

            // load user saved articles ids
            var articleIds = await _repositoryManager.UserArticleRepository
                                                   .Table
                                                   .Where(m => m.UserId == user.Id)
                                                   .OrderByDescending(m => m.Created)
                                                   .Select(m => m.ArticleId)
                                                   .ToListAsync();

            var articles = await _repositoryManager.ArticleRepository
                                                   .Table
                                                   .Include(m => m.Images)
                                                   .Where(m => articleIds.Contains(m.ArticleId) &&
                                                               m.IsPublished == true)
                                                   .OrderByDescending(m => m.ModifiedDate)
                                                   .Skip(startIndex)
                                                   .Take(pageSize.Value)
                                                   .ToListAsync();

            // return simplified models
            return articles.Select(m => m.ToSimpleToModel());

        }

        // get user favorite
        [AllowAnonymous]
        [Route("IsSaved")]
        [HttpPost]
        public async Task<bool> IsSaved([FromBody] UserSaveModel model)
        {
            if (!User.Identity.IsAuthenticated)
            {
                // user is not authenticated
                return false;
            }

            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            // product
            if (model.SaveItemType == 1)
            {
                var savedProduct = await _repositoryManager.UserProductRepository
                                                           .Table
                                                           .SingleOrDefaultAsync(m => m.UserId == user.Id &&
                                                                                      m.ProductId == model.KeyId);

                return savedProduct != null;
            }
            else
            { 
                // article
                var savedArticle = await _repositoryManager.UserArticleRepository
                                                           .Table
                                                           .SingleOrDefaultAsync(m => m.UserId == user.Id &&
                                                                                      m.ArticleId == model.KeyId);
                return savedArticle != null;
            }
                              
        }

        // save user favorite
        // product: type id = 1, article: type id = 2
        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] UserSaveModel model)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);

            if(model.SaveItemType == 1)
            {
                // product
                await SaveProduct(user.Id, model.KeyId);
            }
            else
            {
                await SaveArticle(user.Id, model.KeyId);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("{typeId}/{keyId}")]
        public async Task<IHttpActionResult> Delete(int typeId, int keyId)
        {
            // get user id by user name
            var user = await UserManager.FindByNameAsync(User.Identity.Name);
            
            if(typeId == 1)
            {
                await RemoveProduct(user.Id, keyId);
            }
            else
            {
                await RemoveArticle(user.Id, keyId);
            }

            return Ok();
        }

        private async Task SaveProduct(string userId, int productId)
        {
            var userProduct = await _repositoryManager.UserProductRepository
                                                      .Table
                                                      .SingleOrDefaultAsync(m => m.ProductId == productId &&
                                                                                 m.UserId == userId);

            if(userProduct == null)
            {
                // create
                _repositoryManager.UserProductRepository.Create(new UserProduct
                {
                    UserId = userId,
                    ProductId = productId,
                    Created = DateTime.Now
                });

                await _repositoryManager.UserProductRepository.SaveAsync();
            }

        }

        private async Task RemoveProduct(string userId, int productId)
        {
            var entityToDelete = await _repositoryManager.UserProductRepository
                                                         .Table
                                                         .SingleOrDefaultAsync(m => m.UserId == userId &&
                                                                                    m.ProductId == productId);

            // delete 
            _repositoryManager.UserProductRepository.Delete(entityToDelete);
            await _repositoryManager.UserProductRepository.SaveAsync();
        }

        private async Task SaveArticle(string userId, int articleId)
        {
            var userArticle = await _repositoryManager.UserArticleRepository
                                                      .Table
                                                      .SingleOrDefaultAsync(m => m.ArticleId == articleId &&
                                                                                 m.UserId == userId);

            if (userArticle == null)
            {
                // create
                _repositoryManager.UserArticleRepository.Create(new UserArticle
                {
                    UserId = userId,
                    ArticleId = articleId,
                    Created = DateTime.Now
                });

                await _repositoryManager.UserProductRepository.SaveAsync();
            }
        }

        private async Task RemoveArticle(string userId, int articleId)
        {
            var entityToDelete = await _repositoryManager.UserArticleRepository
                                                         .Table
                                                         .SingleOrDefaultAsync(m => m.UserId == userId &&
                                                                                    m.ArticleId == articleId);

            // delete 
            _repositoryManager.UserArticleRepository.Delete(entityToDelete);
            await _repositoryManager.UserProductRepository.SaveAsync();
        }
    }
}
