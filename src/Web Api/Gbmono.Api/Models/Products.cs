using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gbmono.Api.Models
{
    /// <summary>
    /// simplified product model, only show up major fields to get better performance 
    /// </summary>
    public class ProductSimpleModel
    {
        public int ProductId { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        // 名称
        public string PrimaryName { get; set; }

        // 次要名称
        public string SecondaryName { get; set; }

        public string FullName
        {
            get { return PrimaryName + " " + SecondaryName; }
        }
      
        // 常规价格
        public double Price { get; set; }

        // discount??
        public double? Discount { get; set; }

        public string ImgUrl { get; set; }

        // date
        public DateTime Date { get; set; }
    }
}