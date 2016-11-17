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
    public class ProductTagHelper
    {
        private NestClient<ProductTagDoc> Client
        {
            get
            {
                return new NestClient<ProductTagDoc>().SetIndex(Constants.IndexName.GbmonoV1_producttag).SetType(Constants.TypeName.ProductTag);
            }
        }

        public PagedResponse<ProductTagDoc> SearchByKeyword(PagedRequest<ProductTagSearchRequest> request)
        {
            QueryContainer filter = null;
            var query = new QueryBuilder()
                .OrMatch("name", request.Data.Keyword).Build();
            var result = Client.SetPageNum(request.PageNumber).SetPageSize(request.PageSize).SearchResponse(query, filter);

            return Client.WrapResult(result);
        }

        public PagedResponse<ProductTagDoc> SearchByPrefixKeyword(PagedRequest<ProductTagSearchRequest> request)
        {
            QueryContainer filter = null;
            var query = new QueryBuilder()
                .AndPrefixMatch("name_NA", request.Data.Keyword).Build();
            var result = Client.SetPageNum(request.PageNumber).SetPageSize(request.PageSize).SearchResponse(query, filter);

            return Client.WrapResult(result);
        }
    }
}
