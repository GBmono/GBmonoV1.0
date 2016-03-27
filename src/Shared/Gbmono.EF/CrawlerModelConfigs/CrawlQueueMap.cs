using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.CrawlerModel;

namespace Gbmono.EF.CrawlerModelConfigs
{
    public class CrawlQueueMap : EntityTypeConfiguration<CrawlQueue>
    {

        public CrawlQueueMap()
        {
            ToTable("CrawlQueue");

            HasKey(m => m.Id);
        }
    }
}
