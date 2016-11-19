using Gbmono.Api.Extensions;
using Gbmono.Api.Models;
using Gbmono.Search.IndexManager.IndexHelper;
using Gbmono.Search.IndexManager.SearchHelper;
using Gbmono.Search.ViewModel;
using Gbmono.Search.ViewModel.Requests;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Gbmono.Api.Controllers
{
    [RoutePrefix("api/Search")]
    public class SearchController : ApiController
    {
        private ProductHelper _productHelper;
        private SearchHistoryHelper _searchHistoryHelper;
        
        [HttpPost]
        public async Task<ProductSearchResponse> ProductSearch(PagedRequest<ProductSearchRequest> request)
        {
            return await Task.Run(() =>
            {
                _productHelper = new ProductHelper();
                var searchResult = _productHelper.SearchByKeyword(request);
                var result = new ProductSearchResponse();
                if (request.Data.NeedAggregation)
                {
                    foreach (var agg in searchResult.Aggregation.Aggregations)
                    {
                        foreach (KeyedBucket item in ((BucketAggregate)agg.Value).Items)
                        {
                            switch (agg.Key)
                            {
                                case "agg_brand":
                                    result.BrandList.Add(item.Key);
                                    break;
                                case "agg_category_level_3":
                                    result.CategoryList.Add(item.Key);
                                    break;
                                case "agg_tag":
                                    result.TagList.Add(item.Key);
                                    break;
                            }
                        }
                    }
                }
                
                foreach (var product in searchResult.Data)
                {
                    result.Products.Add(product.ToSimpleModel());
                }
                var userName = User.Identity.IsAuthenticated ? User.Identity.Name : Const.UnAuthorizedUserId;
                Task.Run(async () =>
                {
                    var helper = new SearchHistoryIndexHelper();
                    await helper.IndexDoc(new Search.IndexManager.Documents.SearchHistoryDoc
                    {
                        Keyword = request.Data.Keyword,
                        UserName = userName,
                        SearchType = Search.IndexManager.Documents.SearchType.product
                    });
                });
                
                return result;
            });
        }

        [HttpGet]
        [Route("ProductPrefix/{keyword}")]
        public async Task<IList<string>> ProductPrefix(string keyword)
        {
            return await Task.Run(() =>
            {
                _searchHistoryHelper = new SearchHistoryHelper();
                return _searchHistoryHelper.SearchByPrefixKeyword(keyword);
            });
        }
    }
}