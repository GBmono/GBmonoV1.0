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
            HasMany(m => m.Shops).WithRequired(m=>m.Retailer).HasForeignKey(m => m.RetailderId); // foreign key in same table
        }

    }
}
