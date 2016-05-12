using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.Models
{
    public class ProductBrandCollection
    {
        public int ProductBrandCollectionId{set; get; }
        public int BrandCollectionId { get; set; }
        public BrandCollection BrandCollection { set; get; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
