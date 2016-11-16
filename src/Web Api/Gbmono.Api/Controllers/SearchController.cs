using Gbmono.Api.Extensions;
using Gbmono.Api.Models;
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
        
        [HttpPost]
        public async Task<ProductSearchResponse> ProductSearch([FromBody] PagedRequest<ProductSearchRequest> request)
        {
            return await Task.Run(() =>
            {
                _productHelper = new ProductHelper();
                var searchResult = _productHelper.SearchByKeyword(request);
                var result = new ProductSearchResponse();
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
                foreach (var product in searchResult.Data)
                {
                    result.Products.Add(product.ToSimpleModel());
                }
                
                return result;
            });
        }
    }
}