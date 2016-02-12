using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

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

        public IEnumerable<Retailer> GetAll()
        {
            return _repositoryManager.RetailerRepository.Table.Include(m=>m.Shops).ToList();
        }


        public Retailer GetById(int id)
        {
            // load retailer entity with all the related shops
            return _repositoryManager.RetailerRepository
                                     .Table
                                     .Include(m => m.Shops)
                                     .SingleOrDefault(m => m.RetailerId == id);
        }

    }
}
