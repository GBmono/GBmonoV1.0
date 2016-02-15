using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    /// <summary>
    /// entity map class
    /// map class into actual table in db with specific table name, primary key, foreign key....
    /// </summary>
    public class CategoryMap: EntityTypeConfiguration<Category>
    {
        public CategoryMap()
        {
            ToTable("Category"); // table name in db

            HasKey(m => m.CategoryId); // primary key

            HasOptional(m => m.ParentCategory).WithMany().HasForeignKey(m => m.ParentId); // foreign key in same table
        }
    }
}
