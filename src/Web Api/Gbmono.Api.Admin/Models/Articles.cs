using System;
using System.Collections.Generic;

using Gbmono.EF.Models;

namespace Gbmono.Api.Admin.Models
{
    public class ArticleBindingModel
    {
        public int ArticleId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public IEnumerable<int> TagIds { get; set; }
    }
}