using Gbmono.Search.IndexManager.Builders;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using Gbmono.Search.ViewModel;
using Gbmono.Search.ViewModel.Requests;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.SearchHelper
{
    public class ProductHelper
    {
        private NestClient<ProductDoc> Client
        {
            get
            {
                return new NestClient<ProductDoc>().SetIndex(Constants.IndexName.GbmonoV1_product).SetType(Constants.TypeName.Product);
            }
        }

        public ProductDoc GetProductById(int productId)
        {
            var query = new QueryBuilder().AndTerm("productId", productId).Build();
            var resp = Client.SearchResponse(query);
            return resp.Documents.First();
        }

        public PagedResponse<ProductDoc> SearchByKeyword(PagedRequest<ProductSearchRequest> request)
        {
            //QueryContainer filter = null;
            var matchFields = new string[] { "name^20", "description^5", "instruction" };
            //var categoryLevelMatch = request.Data.FilterCategoryLevel.HasValue ? request.Data.FilterCategoryLevel == 1 ? "categoryLevel1" : request.Data.FilterCategoryLevel == 2 ? "categoryLevel2" : "categoryLevel3" : "categoryLevel3";
            var categoryLevelMatch = "categoryLevel3";
            var query = new QueryBuilder()
                .AndMultiMatch(matchFields, request.Data.Keyword)
                .Build();

            var filterBuilder = new QueryBuilder()
                .AndMatch(categoryLevelMatch, request.Data.CategoryName);
            foreach (var bname in request.Data.BrandName)
            {
                filterBuilder = filterBuilder.OrMatch("brandName", bname);
            }
            foreach (var t in request.Data.Tag)
            {
                filterBuilder = filterBuilder.OrMatch("tags", t);
            }
            var filter = filterBuilder.Build();
            //var query = new QueryBuilder()
            //    .OrMultiMatch(matchFields, request.Data.Keyword)
            //    //.AndMatch("brandName", request.Data.BrandName)
            //    .AndMatch(categoryLevelMatch, request.Data.CategoryName)
            //    .Build();
            
            var categoryAgg = new AggregationContainerDescriptor<ProductDoc>()
                .Terms("agg_brand", f => f.Field("brandName"))
                .Terms("agg_category_level_3", f => f.Field("categoryLevel3"))
                .Terms("agg_tag",f=>f.Field("tags"));
            
            //switch (request.Data.FilterCategoryLevel)
            //{                
            //    case 1:
            //        categoryAgg.Terms("agg_category_level_1", f => f.Field("categoryLevel1"));
            //        break;
            //    case 2:
            //        categoryAgg.Terms("agg_category_level_2", f => f.Field("categoryLevel2"));
            //        break;
            //    default:
            //    case 3:
            //        categoryAgg.Terms("agg_category_level_3", f => f.Field("categoryLevel3"));
            //        break;
            //}
                
            var result = Client.SetPageNum(request.PageNumber).SetPageSize(request.PageSize).SearchResponse(query, filter, a => categoryAgg);
            return Client.WrapResult(result);
        }

        public PagedResponse<ProductDoc> SearchByPrefixKeyword(PagedRequest<ProductSearchRequest> request)
        {
            QueryContainer filter = null;
            var query = new QueryBuilder()
                .AndPrefixMatch("name_NA", request.Data.Keyword).Build();
            var result = Client.SetPageNum(request.PageNumber).SetPageSize(request.PageSize).SearchResponse(query, filter);

            return Client.WrapResult(result);
        }
    }
}
