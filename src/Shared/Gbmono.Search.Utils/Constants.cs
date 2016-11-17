using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.Utils
{
    public class Constants
    {
        public class AppSettingsKeys
        {
            public const string ElasticNodeUrlKey = "elasticNodeUrl";
            public const string ElasticHttpCompressed = "elasticHttpCompressed";
        }

        public class IndexName
        {
            public const string GbmonoV1 = "gbmono_v1";
            public const string GbmonoV1_product = "gbmono_v1_product";
            public const string GbmonoV1_producttag = "gbmono_v1_producttag";
            public const string GbmonoV1_searchhistory = "gbmono_v1_searchhistory";
        }

        public class TypeName
        {
            public const string RetailShop = "retailshop";
            public const string Brand = "brand";
            public const string Product = "product";
            public const string ProductTag = "producttag";
            public const string SearchHistory = "searchhistory";
        }
    }
}
