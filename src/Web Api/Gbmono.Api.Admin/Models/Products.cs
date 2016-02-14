using System;

namespace Gbmono.Api.Admin.Models
{
    public class ProductSimpleModel
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; }

        public int CategoryId { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        // 名称
        public string PrimaryName { get; set; }
        
        // 次要名称
        public string SecondaryName { get; set; }

        // 条形码
        public string BarCode { get; set; }

        // 常规价格
        public double Price { get; set; }

        // discount??
        public double? Discount { get; set; }

        //// 促销 code
        //public string PromotionCode { get; set; }

        //// 优惠券 code
        //public string CuponCode { get; set; }

        //// topic code
        //public string TopicCode { get; set; }

        // 商品激活 (上货架日期)
        public DateTime ActivationDate { get; set; }

        // 商品下货架日期
        public DateTime? ExpiryDate { get; set; }
    }
}