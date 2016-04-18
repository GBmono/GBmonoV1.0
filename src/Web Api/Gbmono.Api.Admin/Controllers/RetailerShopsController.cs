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
    public class RetailerShopsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public RetailerShopsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        [AllowAnonymous]
        public async Task<IEnumerable<RetailerShop>> GetByRetailerId(int id)
        {
            return await _repositoryManager.RetailerShopRepository
                                     .Table
                                     .Where(m => m.RetailerId == id)
                                     .ToListAsync();
        }

    }
}
