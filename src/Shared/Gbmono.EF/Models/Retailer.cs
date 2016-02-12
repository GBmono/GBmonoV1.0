using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 零售商
    /// </summary>
    public class Retailer
    {
        public int RetailerId { get; set; }

        public string Name { get; set; }

        public string LogoUrl { get; set; }

        public bool Enabled { get; set; }

        //retail shops
        public virtual ICollection<RetailShop> Shops { get; set; }

        // type:用来标识改零售商属于境外 境内 跨境通？
    }
}
