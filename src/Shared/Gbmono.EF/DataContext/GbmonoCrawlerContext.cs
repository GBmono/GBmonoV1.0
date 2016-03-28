using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.CrawlerModel;
using Gbmono.EF.CrawlerModelConfigs;

namespace Gbmono.EF.DataContext
{
    public class GbmonoCrawlerContext : DbContext
    {
        public GbmonoCrawlerContext() : base("CrawlerConnection") // connection string name
        {
            this.Database.Initialize(true);
        }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // remove default table name convention
        //    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

        //    // add entity mappings
        //    //modelBuilder.Configurations.Add(new CrawlHistoryMap());

        //    //modelBuilder.Configurations.Add(new CrawlQueueMap());

        //    base.OnModelCreating(modelBuilder);
        //}


        public  DbSet<CrawlHistory> CrawlHistorys { get; set; }
        public  DbSet<CrawlQueue> CrawlQueueMaps { get; set; }
        public  DbSet<KeywordType> KeywordTypes { get; set; }
        public  DbSet<ProcessFileRecord> ProcessFileRecords { get; set; }
        public  DbSet<ProductInfo> ProductInfos { get; set; }
        public  DbSet<Website_KeywordType> Website_KeywordTypes { get; set; }
        public  DbSet<WebsiteName> WebsiteNames { get; set; }


    }
}
