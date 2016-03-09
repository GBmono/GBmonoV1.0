﻿using System;

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
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime IsPublished { get; set; }
    }
}