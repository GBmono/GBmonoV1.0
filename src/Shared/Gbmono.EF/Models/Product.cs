using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        // 条形码
        public string BarCode { get; set; }

        // 商品代码
        public string ProductCode { get; set; }

        // category 商品目录 根据当前目录id可获得上级目录
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        // 品牌 (制造商)
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        // todo: delete -- 产地, map to country
        // public int CountryId { get; set; }
        // public Country Country { get; set; }

        // 名称 (系列名称)
        public string PrimaryName { get; set; }
        
        // 补充名称
        public string SecondaryName { get; set; }

        // 促销 code
        public string PromotionCode { get; set; }

        // 优惠券 code
        public string CuponCode { get; set; }

        // topic code
        public string TopicCode { get; set; }

        // ranking code
        public string RankingCode { get; set; }

        // 容量
        // 以字符表示: 10g X 3 
        public string Capacity { get; set; }

        // 重量 (包括单位)
        public string Weight { get; set; }

        // 商品气味 比如有些商品 含薄荷味
        public string Flavor { get; set; }

        // 材质 / 成分
        // public string Texture { get; set; }

        // 常规价格
        public double Price { get; set; }

        // discount??
        public double? Discount { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }

        public double? Depth { get; set; }

        // four seasons
        public bool? Spring { get; set; }

        public bool? Summer { get; set; }

        public bool? Autumn { get; set; }

        public bool? Winter { get; set; }
        
        // 入库日期
        public DateTime CreatedDate { get; set; }

        // 修改日期
        public DateTime UpdatedDate { get; set; }

        // 商品激活 (上货架日期)
        public DateTime ActivationDate { get; set; }

        // 商品下货架日期
        public DateTime? ExpiryDate { get; set; }

        // 商品描述
        public string Description { get; set; }

        // 使用说明
        public string Instruction { get; set; }

        // 追加文案
        public string ExtraInformation { get; set; }

        // product is unavailable until it's published
        public bool IsPublished { get; set; }

        // 商品图片 (包括使用说明图片)
        public ICollection<ProductImage> Images { get; set; }

        //// 零售商
        //public ICollection<Retailer> Retailers { get; set; }

        //// 网店列表
        //public ICollection<WebShop> WebShops { get; set; }

        // todo:
        // 季节推荐
        // 关联商品
    }
}
