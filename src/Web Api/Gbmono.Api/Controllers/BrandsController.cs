using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;


namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Brands")]
    public class BrandsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public BrandsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        public IEnumerable<Brand> GetAll()
        {
            return _repositoryManager.BrandRepository.Table.OrderBy(m => m.Name).ToList();
        }

        [HttpGet]
        public Brand GetById(int id)
        {
            return _repositoryManager.BrandRepository.Get(id);
        }


    }
}
