using System;

namespace Gbmono.EF.Models
{
    public class ArticleImage
    {
        public int ArticleImageId { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }
        
        public string Url { get; set; }

        public string ThumbnailUrl { get; set; }

        public bool IsCoverImage { get; set; }

    }
}
