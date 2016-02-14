using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;

namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/Countries")]
    public class CountriesController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public CountriesController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        public IEnumerable<Country> GetAll()
        {
            return _repositoryManager.CountryRepository.Table.OrderBy(m => m.Name).ToList();
        }
    }
}
