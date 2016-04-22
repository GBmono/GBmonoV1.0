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
        public Retailer Retailer { get; set; } 

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public int CityId { get; set; }
        public City City { get; set; }

        // public int SuburbId { get; set; }

        public string Address { get; set; }

        //public string Service { set; get; }

        //public string PayWay { set; get; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public string OpenTime { get; set; }

        public string CloseDay { get; set; }

        public string Phone { get; set; }

        public bool Enabled { get; set; }

        public bool TaxFree { set; get; }

        public bool Unionpay { set; get; }


    }
}

