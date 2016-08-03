using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class UserArticleMap: EntityTypeConfiguration<UserArticle>
    {
        public UserArticleMap()
        {
            ToTable("UserArticle");

            HasKey(m => m.UserArticleId);
        }
    }
}
