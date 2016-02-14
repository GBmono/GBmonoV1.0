using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class BrandMap: EntityTypeConfiguration<Brand>
    {
        public BrandMap()
        {
            ToTable("Brand");

            HasKey(m => m.BrandId);
        }
    }
}
