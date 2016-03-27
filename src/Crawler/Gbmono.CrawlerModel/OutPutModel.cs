using System.Collections.Generic;

namespace Gbmono.CrawlerModel
{
    public class OutPutModel
    {
        public string Url { set; get; }

        public string Title { set; get; } 

        public List<string> Images { set; get; }

        public List<string> Ingredients { set; get; }
    }
}