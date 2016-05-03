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
        }

        public class TypeName
        {
            public const string RetailShop = "retailshop";
            public const string Brand = "brand";
        }
    }
}
