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
            modelBuilder.Configurations.Add(new ProductImageMap());
            modelBuilder.Configurations.Add(new ProductEventMap());
            modelBuilder.Configurations.Add(new TagMap());
            modelBuilder.Configurations.Add(new ProductTagMap());
            modelBuilder.Configurations.Add(new UserFavoriteMap());

            modelBuilder.Configurations.Add(new ArticleMap());
            modelBuilder.Configurations.Add(new BrandMap());
            
            modelBuilder.Configurations.Add(new RetailerShopMap());
            modelBuilder.Configurations.Add(new RetailerMap());

            modelBuilder.Configurations.Add(new CountryMap());
            modelBuilder.Configurations.Add(new StateMap());
            modelBuilder.Configurations.Add(new CityMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
