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
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public int? BrandCollectionId { get; set; }
        [String(Analyzer ="ik_max_word",SearchAnalyzer ="ik_max_word")]
        public string Name { get; set; }
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
        public string Description { get; set; }
        public string Instruction { get; set; }
        public string ExtraInformation { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime ActivationDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string Tags { get; set; }
    }
}
