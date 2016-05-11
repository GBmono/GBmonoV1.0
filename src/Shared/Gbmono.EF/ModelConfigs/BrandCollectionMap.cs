using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class BrandCollectionMap: EntityTypeConfiguration<BrandCollection>
    {
        public BrandCollectionMap()
        {
            ToTable("BrandCollection");

            HasKey(m => m.BrandCollectionId);
        }
    }
}
