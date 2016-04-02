using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 零售实体店
    /// </summary>
    public class RetailerShop
    {
        public int RetailShopId { get; set; }

        public int RetailerId { get; set; }
        public Retailer Retailer { get; set; } // 零售商

        public string Name { get; set; }

        // public int CountryId { get; set; }

        public int StateId { get; set; }

        // public int SuburbId { get; set; }

        public string Address { get; set; }

        // public float? Latitude { get; set; }

        // public float? Longitude { get; set; }

        public string OpenTime { get; set; }

        public string CloseDay  { get; set; }

        public string Phone { get; set; }

        public bool? Enabled { get; set; }
    }
}
