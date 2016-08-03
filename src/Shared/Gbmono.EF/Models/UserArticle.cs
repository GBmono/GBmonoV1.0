using System;


namespace Gbmono.EF.Models
{
    public class UserArticle
    {
        public int UserArticleId { get; set; }

        public string UserId { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public DateTime Created { get; set; }
    }
}
