using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 零售实体店
    /// </summary>
    public class RetailShop
    {
        public int RetailShopId { get; set; }

        public int RetailderId { get; set; }
        public Retailer Retailer { get; set; } // 零售商

        public string Name { get; set; }

        public int CountryId { get; set; }

        public int StateId { get; set; }

        public int SuburbId { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public bool Enabled { get; set; }

        // public ICollection<Product> Products { get; set; }
    }
}
