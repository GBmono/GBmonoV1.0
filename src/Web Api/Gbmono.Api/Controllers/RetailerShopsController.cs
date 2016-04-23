﻿using System;
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
using Gbmono.Search.IndexManager.SearchHelper;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.ViewModel;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/RetailerShops")]
    public class RetailerShopsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;
        private readonly RetailShopHelper _retailShopHelper;
        #region ctor
        public RetailerShopsController()
        {
            _repositoryManager = new RepositoryManager();
            _retailShopHelper = new RetailShopHelper();
        }
        #endregion

        //[Route("Retailer/{retailerId}/City/{cityId}")]
        //public async Task<IEnumerable<RetailerShop>> GetByCity(int retailerId, int cityId)
        //{
        //    return await _repositoryManager.RetailerShopRepository
        //                                   .Table
        //                                   .Where(m => m.CityId == cityId && m.RetailerId == retailerId)
        //                                   .OrderBy(m => m.DisplayName)
        //                                   .ToListAsync();
        //}

        [Route("Retailer/{retailerId}/City/{cityId}")]
        public async Task<PagedResponse<RetailShopDoc>> GetByCity(int retailerId, int cityId)
        {            
            return await Task.Run(() =>
            {
                return _retailShopHelper.GetRetailShopDocByCity(cityId, retailerId);
            });            
        }

        [Route("Search")]
        public async Task<IEnumerable<RetailerShop>> Search([FromBody] RetailerShopSearchCriteria model)
        {
            var shops =  _repositoryManager.RetailerShopRepository
                                           .Table
                                           .Where(m => m.RetailerId == model.RetailerId)
                                           .OrderBy(m => m.DisplayName);

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
