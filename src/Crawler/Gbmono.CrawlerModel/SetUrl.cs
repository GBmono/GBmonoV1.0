using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gbmono.CrawlerModel
{
    public class SetUrl
    {
        public string InstanceName { set; get; }
        public string Url { set; get; }
        public int ThirdPartyRecipeUserId { set; get; }
        public string StoreRegex { get; set; }
        public string RecipeRegex { get; set; }
        public string BlockRegex { get; set; }
        public string CrawlDepth { get; set; }
    }
}