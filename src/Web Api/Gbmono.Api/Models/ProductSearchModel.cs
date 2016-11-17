using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gbmono.Api.Models
{
    public class ProductSearchResponse
    {
        public IList<ProductSimpleModel> Products { get; set; } = new List<ProductSimpleModel>();
        public IList<string> BrandList { get; set; } = new List<string>();
        public IList<string> CategoryList { get; set; } = new List<string>();
        public IList<string> TagList { get; set; } = new List<string>();
    }
}