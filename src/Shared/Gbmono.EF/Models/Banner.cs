using System;


namespace Gbmono.EF.Models
{
    /// <summary>
    /// banner
    /// </summary>
    public class Banner
    {
        public int BannerId { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public short BannerType { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public int Width { get; set; }        
        public int Height { get; set; }

        public string RedirectUrl { get; set; }

        public DateTime ActivationDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public string LocationCode { get; set; }

        public bool Enabled { get; set; }

        public int ClickCount { get; set; }
    }
}
