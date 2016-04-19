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
    [RoutePrefix("api/Locations")]
    public class LocationsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;
        #region ctor
        public LocationsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        [Route("{countryId}/States")]
        public async Task<IEnumerable<State>> GetStates(int countryId)
        {
            return await _repositoryManager.StateRepository
                                           .Table
                                           .Where(m => m.CountryId == countryId)
                                           .OrderBy(m => m.DisplayName)
                                           .ToListAsync();
        }

        [Route("{stateId}/Cities")]
        public async Task<IEnumerable<City>> GetCities(int stateId)
        {
            return await _repositoryManager.CityRepository
                                           .Table
                                           .Where(m => m.StateId == stateId)
                                           .OrderBy(m => m.DisplayName)
                                           .ToListAsync();
        }

    }
}
