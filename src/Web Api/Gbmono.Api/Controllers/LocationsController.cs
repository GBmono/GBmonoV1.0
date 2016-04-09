using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Http;
using System.Threading.Tasks;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;

namespace Gbmono.Api.Controllers
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

        [Route("Countries")]
        public IEnumerable<Country> GetCountries()
        {
            throw new NotImplementedException("GetCountries");
        }

        [Route("{countryId}/States")]
        public async Task<IEnumerable<State>>  GetStates(int countryId)
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
