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
        public State State { set; get; }

        public int CityId { get; set; }
        public City City { get; set; }

        public string Address { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public bool Enabled { get; set; }

        public string OpenTime { set; get; }

        public string Service { set; get; }

        public string PayWay { set; get; }

        public string CloseDay { set; get; }

        public string Tel { set; get; }

        // public ICollection<Product> Products { get; set; }
    }
}
