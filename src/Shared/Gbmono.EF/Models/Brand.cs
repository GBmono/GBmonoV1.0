using System;

namespace Gbmono.EF.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        public string Name { get; set; }

        public int ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; } // 品牌商 （制造商）

        public string LogoUrl { get; set; }
    }
}
