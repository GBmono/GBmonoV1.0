using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Gbmono.EF.ModelConfigs;
using Gbmono.EF.Models;

namespace Gbmono.EF.DataContext
{
    public class GbmonoSqlContext : DbContext
    {
        public GbmonoSqlContext() : base("SqlConnection") // connection string name
        {
            
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // remove default table name convention
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // add entity mappings
            modelBuilder.Configurations.Add(new CategoryMap());
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new ManufacturerMap());
            modelBuilder.Configurations.Add(new BrandMap());
            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new RetailShopMap());
            modelBuilder.Configurations.Add(new RetailerMap());
            
            // pls move the config code into seperate config clss
            modelBuilder.Entity<FollowOption>().ToTable("FollowOption");
            modelBuilder.Entity<Banner>().ToTable("Banner");

            base.OnModelCreating(modelBuilder);
        }
    }
}
