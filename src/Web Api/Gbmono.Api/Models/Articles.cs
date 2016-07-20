using System;

namespace Gbmono.Api.Models
{
    /// <summary>
    /// article list item model with conver thumbnail url
    /// </summary>
    public class ArticleSimpleModel
    {
        public int ArticleId { get; set; }

        public short ArticleTypeId { get; set; }

        public string Title { get; set; }

        // conver image url
        public string CoverThumbnailUrl { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set; }

    }
}