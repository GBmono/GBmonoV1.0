﻿using System;

namespace Gbmono.EF.Models
{
    public class ArticleTag
    {
        public int ArticleTagId { get; set; }

        public int ArticleId { get; set; }
        public Article Article { get; set; }

        public int TagId { get; set; }
        public Tag Tag {get; set; }
    }
}
