using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;


namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Categories")]
    public class CategoriesController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public CategoriesController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        // return all categories with hierarchy
        public async Task<IEnumerable<Category>> GetCategories()
        {
            // first level
            var topCates = await _repositoryManager.CategoryRepository
                                           .Table
                                           .Where(m => m.ParentId == null)
                                           .OrderBy(m => m.CategoryCode)
                                           .ToListAsync();
            // add second level categories
            foreach(var topCate in topCates)
            {
                var secondCates = await _repositoryManager.CategoryRepository
                                                          .Table
                                                          .Where(m => m.ParentId == topCate.CategoryId)
                                                          .OrderBy(m => m.CategoryCode)
                                                          .ToListAsync();

                // add third level cates
                foreach(var secondCate in secondCates)
                {
                    var thirdCates = await _repositoryManager.CategoryRepository
                                                          .Table
                                                          .Where(m => m.ParentId == secondCate.CategoryId)
                                                          .OrderBy(m => m.CategoryCode)
                                                          .ToListAsync();

                    secondCate.SubCategories = thirdCates;
                }

                topCate.SubCategories = secondCates;
            }

            return topCates;
        }

        // return top level categories
        [Route("Top")]
        public async Task<IEnumerable<Category>> GetTopCategories()
        {
            return await _repositoryManager.CategoryRepository
                                           .Table
                                           .Where(m => m.ParentId == null)
                                           .OrderBy(m => m.CategoryCode)
                                           .ToListAsync();
        }

        // get brands by top category id
        [Route("{id}/Brands")]
        public async Task<IEnumerable<Brand>> GetBrands(int id)
        {
            // return brands by product top category
            return await _repositoryManager.ProductRepository
                                           .Table
                                           .Include(m => m.Brand)
                                           .Include(m => m.Category.ParentCategory)
                                           .Where(m => m.Category.ParentCategory.ParentId == id)
                                           .Select(m => m.Brand)
                                           .Distinct()
                                           .ToListAsync();
        }

    }
}
