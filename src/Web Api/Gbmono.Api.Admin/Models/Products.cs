using System;
using System.Collections.Generic;

namespace Gbmono.Api.Admin.Models
{
    /// <summary>
    ///  product grid binding model
    /// </summary>
    public class ProductSimpleModel
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; }

        // public int CategoryId { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }

        // 名称
        public string PrimaryName { get; set; }
        
        // 次要名称
        // public string SecondaryName { get; set; }

        // 条形码
        public string BarCode { get; set; }

        // 常规价格
        public double Price { get; set; }

        // discount??
        // public double? Discount { get; set; }

        public string ImgUrl { get; set; }

        public DateTime CreatedDateTime { get; set; }

        // 商品激活 (上货架日期)
        public DateTime ActivationDate { get; set; }

        // 商品下货架日期
        public DateTime? ExpiryDate { get; set; }
    }

    /// <summary>
    /// product search model
    /// </summary>
    public class ProductSearchModel
    {
        public string BarCode { get; set; }

        public string FullProductCode { get; set; }
    }

    /// <summary>
    /// product image upload model
    /// </summary>
    public class ProductImageUploadModel
    {
        public int ProductId { get; set; }

        public short ProductImageTypeId { get; set; }

        public string ImageName { get; set; }
    }

    /// <summary>
    /// product tags save model
    /// </summary>
    public class ProductTagSaveModel
    {
        public int ProductId { get; set; }

        public IEnumerable<int> TagIds { get; set; }
    }
}