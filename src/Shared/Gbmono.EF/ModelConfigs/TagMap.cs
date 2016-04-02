using System.Data.Entity.ModelConfiguration;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class TagMap: EntityTypeConfiguration<Tag>
    {
        public TagMap()
        {
            ToTable("Tag");

            HasKey(m => m.TagId);
        }
    }
}
