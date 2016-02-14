using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;

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

    }
}
