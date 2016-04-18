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
    [RoutePrefix("api/RetailerShops")]
    [AllowAnonymous]
    public class RetailerShopsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public RetailerShopsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        [Route("Retailer/{retailerId}")]
        public async Task<IEnumerable<RetailerShop>> GetByRetailerId(int retailerId)
        {
            return await _repositoryManager.RetailerShopRepository
                                     .Table
                                     .Where(m => m.RetailerId == retailerId)
                                     .ToListAsync();
        }

        public async Task<RetailerShop> GetById(int id)
        {
            return Task.Run(() => _repositoryManager.RetailerShopRepository
                .Table.Single(m => m.RetailShopId == id)).Result;
        }


        [HttpPut]
        public IHttpActionResult Update(int id, [FromBody]RetailerShop retailerShop)
        {
            _repositoryManager.RetailerShopRepository.Update(retailerShop);
            _repositoryManager.RetailerShopRepository.Save();

            return Ok(retailerShop.RetailShopId);
        }
    }
}
