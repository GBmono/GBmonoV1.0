using System;
using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class UserFavoriteMap: EntityTypeConfiguration<UserFavorite>
    {
        public UserFavoriteMap()
        {
            ToTable("UserFavorite");

            HasKey(m => m.UserFavoriteId);
        }
    }
}
