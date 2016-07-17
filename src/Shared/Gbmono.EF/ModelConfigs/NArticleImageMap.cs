using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class NArticleImageMap : EntityTypeConfiguration<NArticleImage>
    {
        public NArticleImageMap()
        {
            ToTable("NArticleImage");

            HasKey(m => m.NArticleImageId);
        }
    }
}
