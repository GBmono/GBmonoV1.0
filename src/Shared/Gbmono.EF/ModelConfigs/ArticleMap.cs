using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class ArticleMap: EntityTypeConfiguration<Article>
    {
        public ArticleMap()
        {
            ToTable("Article");

            HasKey(m => m.ArticleId);
        }
    }
}
