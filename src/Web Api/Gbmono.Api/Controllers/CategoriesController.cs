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

        public IEnumerable<Category> GetAll()
        {
            var categories = _repositoryManager.CategoryRepository.Table.ToList();

            // level 1 categories
            var topCategories = categories.Where(m => m.ParentId == null);
            // level 2 categories
            foreach (var topCate in topCategories)
            {
                // level 2
                var subcategories = categories.Where(m => m.ParentId == topCate.CategoryId);

                // attach level 2 categories into top categories
                topCate.SubCategories = subcategories;

                // level 3 categories
                foreach (var subCate in subcategories)
                {
                    // level 3
                    var cates = categories.Where(m => m.ParentId == subCate.CategoryId);

                    // attch
                    subCate.SubCategories = cates;
                }
            }

            return topCategories;
        }

        [Route("GetFilterCategories/{categoryId}")]
        public async Task<IEnumerable<Category>> GetFilterCategories(int categoryId)
        {
            return await _repositoryManager.CategoryRepository
                                           .Table
                                           .Where(m => m.ParentId == categoryId)
                                           .ToListAsync();

            //return await Task.Run(() =>
            //{
            //    return _repositoryManager.CategoryRepository.Fetch(f => f.ParentId == categoryId).ToList();
            //});
        }
    }
}
