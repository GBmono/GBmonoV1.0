using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            ToTable("Product"); // table name in db

            HasKey(m => m.ProductId); // primary key
            //HasMany(m => m.WebShops).WithOptional().HasForeignKey(m => m.WebShopId);

            // HasOptional(m => m.ParentCategory).WithMany().HasForeignKey(m => m.ParentId); // foreign key in same table

            //Wroking
            HasMany(m => m.Retailers).WithMany().Map(m =>
                       {
                           m.ToTable("ProductRetailer");
                           m.MapLeftKey("ProductId");
                           m.MapRightKey("RetailerId");
                       });

            HasMany(m => m.WebShops).WithMany().Map(m =>
                        {
                            m.ToTable("ProductWebShop");
                            m.MapLeftKey("ProductId");
                            m.MapRightKey("WebShopId");
                        });

            HasMany(m => m.Images).WithRequired(m => m.Product).HasForeignKey(m=>m.ProductId);
        }
    }
}
