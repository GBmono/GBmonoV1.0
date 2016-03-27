﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.CrawlerModel
{
    public class WebsiteName
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string SourceWebsite { get; set; }
        public int ThirdPartyUserId { get; set; }
        public int GroupId { get; set; }

        //public virtual ICollection<ProductInfo> ProductInfos { get; set; }

        //public virtual ICollection<Website_KeywordType> Website_KeywordType { get; set; }

    }
}