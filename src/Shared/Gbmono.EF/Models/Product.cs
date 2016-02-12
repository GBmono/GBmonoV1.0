using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; }

        // category 商品目录 根据当前目录id可获得上级目录
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        // 品牌，根据品牌可以取得上级的制造商(品牌商)
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        // 产地, map to country
        public int CountryId { get; set; }
        public Country Country { get; set; }

        // 名称
        public string PrimaryName { get; set; }
        // 次要名称
        public string SecondaryName { get; set; }

        // 商品气味 比如有些商品 含薄荷味
        public string Flavor { get; set; }

        // 容量
        // 以字符表示: 10g X 3 
        public string Content { get; set; }

        // 重量
        public double Weight { get; set; }

        // 重量单位
        public string WeightUnit { get; set; }

        // 体积 15x18x100mm
        public string Shape { get; set; }
        
        // 材质 / 成分
        public string Texture { get; set; }

        //商品排列Code
        public int? DisplayCode { get; set; }

        // 条形码
        public string BarCode { get; set; }

        // 常规价格
        public double Price { get; set; }

        // discount??
        public double? Discount { get; set; }

        // 商品描述
        public string Description { get; set; }
        public ICollection<ProductImage> DescriptionImages { get; set; }

        // 使用说明
        public string Instruction { get; set; }
        public ICollection<ProductImage> InstructionImages { get; set; }

        // 追加文案
        public string ExtraInformation { get; set; }

        // 商品图片
        public ICollection<ProductImage> Images { get; set; }

        // 零售商
        public ICollection<Retailer> Retailers { get; set; }

        // 网店列表
        public ICollection<WebShop> WebShops { get; set; }

        // 促销 code
        public string PromotionCode { get; set; }

        // 优惠券 code
        public string CuponCode { get; set; }

        // topic code
        public string TopicCode { get; set; }

        // 入库日期
        public DateTime CreatedDate { get; set; }
        
        // 修改日期
        public DateTime UpdatedDate { get; set; }

        // 商品激活 (上货架日期)
        public DateTime ActivationDate { get; set; }
        
        // 商品下货架日期
        public DateTime ExpiryDate { get; set; }
     
        // todo:
        // 季节推荐
        // 关联商品
    }
}
