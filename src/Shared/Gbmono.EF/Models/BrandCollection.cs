using System;


namespace Gbmono.EF.Models
{
    public class BrandCollection
    {
        public int BrandCollectionId { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }
    }
}
