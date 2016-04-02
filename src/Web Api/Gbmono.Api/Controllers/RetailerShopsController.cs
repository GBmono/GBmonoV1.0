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
using Gbmono.Api.Models;


namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/RetailerShops")]
    public class RetailerShopsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public RetailerShopsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        
        [Route("Search")]
        public async Task<IEnumerable<RetailerShop>> Search([FromBody] RetailerShopSearchCriteria model)
        {
            var shops =  _repositoryManager.RetailerShopRepository
                                                .Table
                                                .Where(m => m.RetailerId == model.RetailerId);

            // if retailer is not available
            if (!shops.Any())
            {
                return shops;
            }

            // search by address or name
            return await shops.Where(m => m.Address.Contains(model.Keyword) || m.Name.Contains(model.Keyword)).ToListAsync();
        }
    }
}
