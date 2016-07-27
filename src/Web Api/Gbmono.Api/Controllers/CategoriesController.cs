using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Threading.Tasks;
using System.Web.Http;
using System.Data.Entity;
using System.IO;
using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Models;
using Newtonsoft.Json;

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
            foreach (var topCate in topCates)
            {
                var secondCates = await _repositoryManager.CategoryRepository
                                                          .Table
                                                          .Where(m => m.ParentId == topCate.CategoryId)
                                                          .OrderBy(m => m.CategoryCode)
                                                          .ToListAsync();

                // add third level cates
                foreach (var secondCate in secondCates)
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

        // get by id
        public async Task<Category> GetById(int id)
        {
            return await _repositoryManager.CategoryRepository.GetAsync(id);
        }

        // todo: to be deprecated
        // return category menu binding models
        [Route("Menu/{id}")]
        public async Task<CategoryMenu> GetCategoryMenuItems(int id)
        {
            var selectedCategory = await _repositoryManager.CategoryRepository.GetAsync(id);

            // expanded subcategories
            var subItems = await _repositoryManager.CategoryRepository
                                                   .Table
                                                   .Where(m => m.ParentId == selectedCategory.CategoryId)
                                                   .OrderBy(m => m.CategoryCode)
                                                   .ToListAsync();

            var expendedItem = new ExpandedCategoryMenuItem
            {
                CategoryId = selectedCategory.CategoryId,
                Name = selectedCategory.Name,
                SubItems = subItems.Select(m => new CategoryMenuItem { CategoryId = m.CategoryId, Name = m.Name })
            };

            var topcates = await _repositoryManager.CategoryRepository
                                                         .Table
                                                         .Where(m => m.ParentId == null && m.CategoryId != selectedCategory.CategoryId)
                                                         .OrderBy(m => m.CategoryCode)
                                                         .ToListAsync();

            // covert into binding model
            var collapsedItems = topcates.Select(m => new CategoryMenuItem { CategoryId = m.CategoryId, Name = m.Name });

            return new CategoryMenu { ExpandedItem = expendedItem, CollapsedItems = collapsedItems };
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

        // todo: to be deprecated
        // return third level categories by second or top category id
        [Route("Third/{id}")]
        public async Task<IEnumerable<Category>> GetThirdCategories(int id)
        {
            // if it's top category
            var category = await _repositoryManager.CategoryRepository.GetAsync(id);
            if (category.ParentId == null)
            {
                return await _repositoryManager.CategoryRepository
                                               .Table
                                               .Include(m => m.ParentCategory)
                                               .Where(m => m.ParentCategory.ParentId == id)
                                               .OrderBy(m => m.CategoryCode)
                                               .ToListAsync();
            }

            // if it's second category
            return await _repositoryManager.CategoryRepository
                                               .Table
                                               .Where(m => m.ParentId == id)
                                               .OrderBy(m => m.CategoryCode)
                                               .ToListAsync();
        }

        // get brands by top category id
        [Route("{id}/Brands/{levelId}")]
        public async Task<IEnumerable<Brand>> GetBrands(int id, int levelId)
        {
            if(levelId == 1)
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
            else if(levelId == 2)
            {
                // return brands by product second level category
                return await _repositoryManager.ProductRepository
                                               .Table
                                               .Include(m => m.Brand)
                                               .Include(m => m.Category.ParentCategory)
                                               .Where(m => m.Category.ParentId == id)
                                               .Select(m => m.Brand)
                                               .Distinct()
                                               .ToListAsync();
            }
            else
            {
                // return brands by third level category
                return await _repositoryManager.ProductRepository
                                               .Table
                                               .Include(m => m.Brand)
                                               .Where(m => m.CategoryId == id)
                                               .Select(m => m.Brand)
                                               .Distinct()
                                               .ToListAsync();
            }

        }

    }
}
