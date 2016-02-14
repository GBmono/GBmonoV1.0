using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class ProductImageMap: EntityTypeConfiguration<ProductImage>
    {
        public ProductImageMap()
        {
            ToTable("ProductImage");

            HasKey(m => m.ProductImageId);
        }
    }
}
