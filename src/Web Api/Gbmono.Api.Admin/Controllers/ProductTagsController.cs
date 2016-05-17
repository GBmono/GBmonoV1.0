using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Threading.Tasks;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Admin.HttpResults;
using Gbmono.Api.Admin.Models;

namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/ProductTags")]
    public class ProductTagsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public ProductTagsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion


        [Route("Product/{id}")]
        public async Task<List<ProductTag>> GetById(int id)
        {
            return await _repositoryManager.ProductTagRepository
                                           .Table
                                           .Include(m=>m.Tag)
                                           .Where(m => m.ProductId == id).ToListAsync();
        }

    }
}
