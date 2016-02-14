using System;
using System.ComponentModel;

namespace Gbmono.EF.Models
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }

        public int ProductId { set; get; }
        public Product Product { set; get; }

        public string Name { get; set; }

        public string FileName { get; set; }

        public bool? IsPrimary { get; set; }

        public bool? IsThumbnail { get; set; }

        public short? ProductImageTypeId { get; set; }
    }
}
