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

            // third
            if (_repositoryManager.CategoryRepository.Table.Any(s => s.ParentId == id))
            {
                return "#/categories/" + categoryId + "/third";
            }

            return "#/categories/" + categoryId + "/products";
        }
    }
}
