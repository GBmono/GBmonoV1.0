using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class ArticleTagMap: EntityTypeConfiguration<ArticleTag>
    {
        public ArticleTagMap()
        {
            ToTable("ArticleTag");

            HasKey(m => m.ArticleTagId);
        }
    }
}
