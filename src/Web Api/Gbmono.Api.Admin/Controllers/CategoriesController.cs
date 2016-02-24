using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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
        public IEnumerable<Category> GetTopCategories()
        {
            return _repositoryManager.CategoryRepository
                                     .Table
                                     .Where(m => m.ParentId == null).OrderBy(m => m.CategoryCode).ToList();
        }

        [Route("Parent/{id}")]
        public IEnumerable<Category> GetCategoriesByParent(int id)
        {
            return _repositoryManager.CategoryRepository
                                     .Table
                                     .Where(m => m.ParentId == id).OrderBy(m => m.CategoryCode).ToList();
        }

        #region category tree
        [Route("Treeview/{id:int?}")]
        public IEnumerable<KendoTreeViewItem> GetCategories(int? id = null)
        {
            return _repositoryManager.CategoryRepository
                                     .Table
                                     .Where(m => m.ParentId == id)
                                     .OrderBy(m => m.CategoryCode)
                                     .Select(m => new KendoTreeViewItem
                                     {
                                         Id = m.CategoryId,
                                         Name = m.Name,
                                         Expanded = false,
                                         HasChildren = _repositoryManager.CategoryRepository.Table.Any(s => s.ParentId == m.CategoryId),
                                         LinksTo = _repositoryManager.CategoryRepository.Table.Any(s => s.ParentId == m.CategoryId) 
                                                   ? "" 
                                                   : "#/categories/" + m.CategoryId + "/products" // only return link when the category is leaf level
                                     })
                                     .ToList();
        }
        #endregion

    }
}
