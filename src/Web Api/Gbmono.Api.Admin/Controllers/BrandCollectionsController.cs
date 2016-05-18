using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Gbmono.EF.Infrastructure;
using Gbmono.EF.Models;

namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/BrandCollections")]

    public class BrandCollectionsController : ApiController
    {

        private readonly RepositoryManager _repositoryManager;
        #region ctor
        public BrandCollectionsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion


        [Route("Brand/{id}")]
        public async Task<IEnumerable<BrandCollection>> GetByBrand(int id)
        {
            return await _repositoryManager.BrandCollectionRepository.Table.Where(m => m.BrandId == id).ToListAsync();
        }



    }
}
