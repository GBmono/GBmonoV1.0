
namespace Gbmono.CrawlerModel
{
    public class CrawlInstanceNameUrl
    {
        public string InstanceName { set; get; }

        public string WebSiteUrl { set; get; }

        public int CrawlHistoryCount { set; get; }

        //public int ProcessedCount { set; get; }
        public int PersistentCount { set; get; }
        
        public int BloggerUserId { set; get; }

        public int GroupId { set; get; }
    }
}