using System;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 新闻, 咨询
    /// </summary>
    public class News
    {
        public int NewsId { get; set; }

        public string Title { get; set; }

        public string TilteImageUrl { get; set; }

        // html code
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime IsPublished { get; set; }

        public string PulishedDate { get; set; }

        public DateTime PublishDate { get; set; }
    }
}
