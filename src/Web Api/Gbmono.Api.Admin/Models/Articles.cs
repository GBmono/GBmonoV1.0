﻿using System;
using System.Collections.Generic;

using Gbmono.EF.Models;

namespace Gbmono.Api.Admin.Models
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

        public bool IsPublished { get; set; }
    }

    public class ArticleBindingModel
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<int> TagIds { get; set; }
    }
}