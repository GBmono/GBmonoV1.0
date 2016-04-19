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
    public class RetailersController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        public RetailersController()
        {
            _repositoryManager = new RepositoryManager();
        }

        public async Task<IEnumerable<Retailer>> GetAll()
        {
            return await _repositoryManager.RetailerRepository.Table.ToListAsync();
        }
    }
}
