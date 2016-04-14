using Gbmono.Search.Utils;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Documents
{
    [ElasticsearchType(IdProperty = "RetailShopId",Name =Constants.TypeName.RetailShop)]
    public class RetailShopDoc
    {
        public int RetailShopId { get; set; }
        public int RetailerId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public int CityId { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string OpenTime { get; set; }
        public string CloseDay { get; set; }
        public string Phone { get; set; }
        public bool Enabled { get; set; }
        public bool TaxFree { get; set; }
        public bool Unionpay { get; set; }
    }
}
