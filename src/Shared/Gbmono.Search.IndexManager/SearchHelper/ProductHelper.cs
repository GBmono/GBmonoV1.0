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
        private NestClient<ProductDoc> CLient
        {
            get
            {
                return new NestClient<ProductDoc>().SetIndex(Constants.IndexName.GbmonoV1_product).SetType(Constants.TypeName.Product);
            }
        }

        public ProductDoc GetProductById(int productId)
        {
            var query = new QueryBuilder().AndTerm("productId", productId).Build();
            var resp = CLient.SearchResponse(query);
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
            var result = CLient.SetPageNum(request.PageNumber).SetPageSize(request.PageSize).SearchResponse(query, filter, a => categoryAgg);

            return CLient.WrapResult(result);
        }
    }
}
