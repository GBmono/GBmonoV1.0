using Gbmono.Search.IndexManager.Builders;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
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


        public RetailShopDoc GetRetailShopDocById(int shopId)
        {
            var query = new QueryBuilder().AndTerm("retailShopId", shopId).Build();
            var resp = Client.SearchResponse(query);
            return resp.Documents.First();
        }
    }
}
