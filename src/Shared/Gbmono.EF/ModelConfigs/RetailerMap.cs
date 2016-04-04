using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class RetailerMap : EntityTypeConfiguration<Retailer>
    {
        public RetailerMap()
        {
            ToTable("Retailer"); // table name in db

            HasKey(m => m.RetailerId); // primary key            
        }

    }
}
