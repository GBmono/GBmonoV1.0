using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Retailers")]
    public class RetailersController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public RetailersController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        public async Task<IEnumerable<Retailer>> GetAll()
        {
            return await _repositoryManager.RetailerRepository.Table.ToListAsync();
        }

    }
}
