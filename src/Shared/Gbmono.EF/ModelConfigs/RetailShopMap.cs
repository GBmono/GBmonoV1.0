using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class RetailShopMap : EntityTypeConfiguration<RetailShop>
    {
        public RetailShopMap()
        {
            ToTable("RetailerShop"); // table name in db

            HasKey(m => m.RetailShopId); // primary key
            // HasOptional(m => m.ParentCategory).WithMany().HasForeignKey(m => m.ParentId); // foreign key in same table
        }
    }
}
