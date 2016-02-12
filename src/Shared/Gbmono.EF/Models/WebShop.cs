using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 网店
    /// </summary>
    public class WebShop
    {
        public int WebShopId { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public string LogoUrl { get; set; }

        public bool Enabled { get; set; }

    }
}
