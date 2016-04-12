using Gbmono.Search.IndexManager;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexBuilder.Builder
{
    public class RetailerShopsBuilder
    {
        private NestClient<RetailShopDoc> Client
        {
            get
            {
                return new NestClient<RetailShopDoc>().SetIndex(Constants.IndexName.GbmonoV1).SetType(Constants.TypeName.RetailShop);
            }
        }
    }
}
