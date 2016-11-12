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
            QueryContainer filter = null;
            var matchFields = new string[] { "name^4", "description^2", "instruction" };
            var query = new QueryBuilder()
                .OrMultiMatch(matchFields, request.Data.Keyword)
                .Build();
            var categoryAgg = new AggregationContainerDescriptor<ProductDoc>().Terms("agg_category", f => f.Field("categories"));
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
