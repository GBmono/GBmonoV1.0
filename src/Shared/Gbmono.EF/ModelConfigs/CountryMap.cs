using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;


namespace Gbmono.EF.ModelConfigs
{
    public class CountryMap : EntityTypeConfiguration<Country>
    {
        public CountryMap()
        {
            ToTable("Country");

            HasKey(m => m.CountryId);
        }
    }
}
