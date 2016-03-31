using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class ProductTagMap: EntityTypeConfiguration<ProductTag>
    {
        public ProductTagMap()
        {
            ToTable("ProductTag");

            HasKey(m => m.ProductTagId);
        }
    }
}
