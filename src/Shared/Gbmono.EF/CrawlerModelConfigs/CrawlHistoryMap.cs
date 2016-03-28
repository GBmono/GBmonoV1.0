using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.CrawlerModel;
using Gbmono.EF.Models;

namespace Gbmono.EF.CrawlerModelConfigs
{
    public class CrawlHistoryMap : EntityTypeConfiguration<CrawlHistory>
    {
        public CrawlHistoryMap()
        {
            ToTable("CrawlHistory");

            HasKey(m => m.Id);
        }
    }
}
