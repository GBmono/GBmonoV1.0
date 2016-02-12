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
    [RoutePrefix("api/Manufacturers")]
    public class ManufacturersController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public ManufacturersController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        public IEnumerable<Manufacturer> GetAll()
        {
            return _repositoryManager.ManufacturerRepository.Table.OrderBy(m => m.Name).ToList();
        }

        public Manufacturer GetById(int id)
        {
            // return _repositoryManager.ManufacturerRepository.Get(id);

            // load manufacturer entity with all the related brands data
            return _repositoryManager.ManufacturerRepository
                                     .Table
                                     .Include(m => m.Brands)
                                     .SingleOrDefault(m => m.ManufacturerId == id);
        }

    }
}
