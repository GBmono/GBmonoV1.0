using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class UserProductMap: EntityTypeConfiguration<UserProduct>
    {
        public UserProductMap()
        {
            ToTable("UserProduct");

            HasKey(m => m.UserProductId);
        }
    }
}
