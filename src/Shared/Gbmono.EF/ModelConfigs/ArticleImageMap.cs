using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class ArticleImageMap: EntityTypeConfiguration<ArticleImage>
    {
        public ArticleImageMap()
        {
            ToTable("ArticleImage");

            HasKey(m => m.ArticleImageId);
        }
    }
}
