using System.Collections.Generic;

namespace Gbmono.CrawlerModel
{
    public class OriginalWebSiteTxt
    {
        public string OriginalWebSite { get; set; }
        public string ThridPartyUserId { get; set; }
        public List<string> StoreRegex { get; set; }
        public List<string> RecipeRegex { get; set; }
        public List<string> BlockRegex { get; set; }
    }
}
