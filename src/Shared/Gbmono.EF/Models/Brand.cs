using System;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 品牌 (制造商)
    /// </summary>
    public class Brand
    {
        public int BrandId { get; set; }

        public string Name { get; set; }

        public string BrandCode { get; set; }

        public string LogoUrl { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}
