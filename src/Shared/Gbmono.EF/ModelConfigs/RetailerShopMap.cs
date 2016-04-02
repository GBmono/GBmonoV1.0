using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class RetailerShopMap : EntityTypeConfiguration<RetailerShop>
    {
        public RetailerShopMap()
        {
            ToTable("RetailerShop"); // table name in db

            HasKey(m => m.RetailShopId); // primary key            
        }
    }
}
