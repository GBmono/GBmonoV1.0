using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Admin.Models;
using System.Data.Entity;
using Gbmono.Api.Admin.HttpResults;

namespace Gbmono.Api.Admin.Controllers
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

        public Category GetById(int id)
        {
            return _repositoryManager.CategoryRepository
                                     .Table
                                     .Include(m => m.ParentCategory.ParentCategory)
                                     .SingleOrDefault(m => m.CategoryId == id);
        }

        [AllowAnonymous]
        [Route("Top")]
        public async Task<IEnumerable<Category>> GetTopCategories()
        {
            return await _repositoryManager.CategoryRepository
                                           .Table
                                           .Where(m => m.ParentId == null)
                                           .OrderBy(m => m.CategoryCode)
                                           .ToListAsync();
        }

        [Route("Parent/{id}")]
        public async Task<IEnumerable<Category>>  GetCategoriesByParent(int id)
        {
            return await _repositoryManager.CategoryRepository
                                           .Table
                                           .Where(m => m.ParentId == id)
                                           .OrderBy(m => m.CategoryCode)
                                           .ToListAsync();
        }

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Category category)
        {
            if(_repositoryManager.CategoryRepository
                                 .Table
                                 .Any(m => m.CategoryCode == category.CategoryCode && m.Name == category.Name))
            {
                return new DataInvalidResult("Name or code already exists.", Request);
            }

            // create
            _repositoryManager.CategoryRepository.Create(category);
            await _repositoryManager.CategoryRepository.SaveAsync();

            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> Update(int id, [FromBody] Category category)
        {
            //if (_repositoryManager.CategoryRepository
            //         .Table
            //         .Any(m => m.CategoryId != category.CategoryId &&
            //                   (m.CategoryCode == category.CategoryCode || m.Name == category.Name)))
            //{
            //    return new DataInvalidResult("Name or code already exists.", Request);
            //}

            // update
            _repositoryManager.CategoryRepository.Update(category);
            await _repositoryManager.CategoryRepository.SaveAsync();

            return Ok();
        }

        #region category tree
        [AllowAnonymous]
        [Route("Treeview/{id:int?}")]
        public IEnumerable<KendoTreeViewItem> GetCategories(int? id = null)
        {
            // get categories
            var categories = _repositoryManager.CategoryRepository
                                     .Table
                                     .Where(m => m.ParentId == id)
                                     .OrderBy(m => m.CategoryCode)
                                     .ToList();
            // convert into
            var treeviewItems = categories.Select(m => new KendoTreeViewItem
                                     {
                                         Id = m.CategoryId,
                                         Name = m.Name,
                                         Expanded = false,
                                         HasChildren = _repositoryManager.CategoryRepository.Table.Any(s => s.ParentId == m.CategoryId),
                                         LinksTo = BuildTreeviewUrl(id, m.CategoryId)
                                     })
                                     .ToList();

            return treeviewItems;
        }
        #endregion

        private string BuildTreeviewUrl(int? id, int categoryId)
        {
            // id is top category id
            if(id == null)
            {
                return "#/categories/" + categoryId + "/second";
            }

            // check if the current category id is second level or third level
            if (_repositoryManager.ProductRepository.Table.Any(s => s.CategoryId == categoryId))
            {
                return "#/categories/" + categoryId + "/products";                
            }

            return "#/categories/" + categoryId + "/third";
        }
    }
}
