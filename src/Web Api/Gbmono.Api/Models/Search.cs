using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gbmono.Api.Models
{
    /// <summary>
    /// retail shop search model
    /// </summary>
    public class RetailerShopSearchCriteria
    {
        public int RetailerId { get; set; }

        public string Keyword { get; set; }
    }
}