using Gbmono.Search.IndexManager.Builders;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using Gbmono.Search.ViewModel;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.SearchHelper
{
    public class RetailShopHelper
    {
        private NestClient<RetailShopDoc> Client
        {
            get
            {
                return new NestClient<RetailShopDoc>().SetIndex(Constants.IndexName.GbmonoV1).SetType(Constants.TypeName.RetailShop);
            }
        }
        //TODO: define search field, aggregation field

        public RetailShopDoc GetRetailShopDocById(int shopId)
        {
            var query = new QueryBuilder().AndTerm("retailShopId", shopId).Build();
            var resp = Client.SearchResponse(query);
            return resp.Documents.First();
        }

        //public RetailShopDoc GetRetailShopDocByCity(int cityId, int? retailerId = null)
        //public ISearchResponse<RetailShopDoc> GetRetailShopDocByCity(int cityId, int? retailerId = null)
        public PagedResponse<RetailShopDoc> GetRetailShopDocByCity(int cityId, int? retailerId = null)
        {
            var fb = new FilterBuilder().AndTerm("cityId", cityId);
            if (retailerId.HasValue)
            {
                fb.AndTerm("retailerId", retailerId.Value);
            }
            var filter = fb.Build();
            // maybe add size 
            var cityAgg = new AggregationContainerDescriptor<RetailShopDoc>().Terms("agg_city", f => f.Field("cityId"));
            var result = Client.SearchResponse(filter: filter, aggregation: a => cityAgg);
            return Client.WrapResult(result);                   
        }
    }
}
