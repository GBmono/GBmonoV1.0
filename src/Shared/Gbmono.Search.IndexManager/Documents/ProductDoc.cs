using Gbmono.Search.Utils;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Documents
{
    [ElasticsearchType(IdProperty = "ProductId", Name = Constants.TypeName.Product)]
    public class ProductDoc
    {
        public int ProductId { get; set; }
        public string Categories { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public int? BrandCollectionId { get; set; }
        public string BrandCollectionName { get; set; }
        [String(Analyzer ="ik_max_word",SearchAnalyzer ="ik_max_word")]
        public string Name { get; set; }
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string Name_NA { get; set; }
        public string PromotionCode { get; set; }
        public string CuponCode { get; set; }
        public string TopicCode { get; set; }
        public string RankingCode { get; set; }
        public string Capacity { get; set; }
        public string Weight { get; set; }
        public string Flavor { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public double? Depth { get; set; }
        public double Price { get; set; }
        public bool? Spring { get; set; }
        public bool? Summer { get; set; }
        public bool? Autumn { get; set; }
        public bool? Winter { get; set; }
        public double? Discount { get; set; }
        [String(Analyzer = "ik_max_word", SearchAnalyzer = "ik_max_word")]
        public string Description { get; set; }
        [String(Analyzer = "ik_max_word", SearchAnalyzer = "ik_max_word")]
        public string Instruction { get; set; }
        [String(Analyzer = "ik_max_word", SearchAnalyzer = "ik_max_word")]
        public string ExtraInformation { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Tags { get; set; }
        public List<ProductImageDoc> Images { get; set; }        
    }

    public class ProductImageDoc
    {
        public int ProductImageId { get; set; }
        public string Name { get; set; }
        public string FileName { get; set; }
        public int ProductId { get; set; }
        public bool? IsPrimary { get; set; }
        public bool? IsThumbnail { get; set; }
        public short? ProductImageTypeId { get; set; }
    }
}
