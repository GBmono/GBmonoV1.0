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
    }
}
