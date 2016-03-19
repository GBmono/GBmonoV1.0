using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;

using Gbmono.EF.Models;
using Gbmono.EF.Infrastructure;
using Gbmono.Api.Admin.Models;

namespace Gbmono.Api.Admin.Controllers
{
    [RoutePrefix("api/Statistics")]
    public class StatisticsController : ApiController
    {
        private readonly RepositoryManager _repositoryManager;

        #region ctor
        public StatisticsController()
        {
            _repositoryManager = new RepositoryManager();
        }
        #endregion

        [Route("Site")]
        public IEnumerable<SiteStatsItem> GetSiteStats()
        {
            var resultSet = new List<SiteStatsItem>();

            // count of top category
            var categoryCount = _repositoryManager.CategoryRepository
                                                  .Table
                                                  .Count(m => m.ParentId == null);
            resultSet.Add(new SiteStatsItem { Name = "大分类", Value = categoryCount });

            // product
            var productCount = _repositoryManager.ProductRepository
                                                 .Table
                                                 .Count();
            resultSet.Add(new SiteStatsItem { Name = "商品", Value = productCount });

            // brand
            var brandCount = _repositoryManager.BrandRepository
                                               .Table
                                               .Count();
            resultSet.Add(new SiteStatsItem { Name = "品牌商", Value = brandCount });

            // todo: user, retailers

            return resultSet;
        }
    }
}
