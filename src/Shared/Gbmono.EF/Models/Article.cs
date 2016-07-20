using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 新闻, 咨询
    /// </summary>
    public class Article
    {
        public int ArticleId { get; set; }

        public short ArticleTypeId { get; set; }

        public string Title { get; set; }

        // html code
        public string Body { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsPublished { get; set; }

        // article images
        public ICollection<ArticleImage> Images { get; set; }
        
        // article tags
        // public IEnumerable<ArticleTag> Tags { get; set; }

    }
}
