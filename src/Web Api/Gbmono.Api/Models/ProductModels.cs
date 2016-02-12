using System;
using System.Collections.Generic;
using Gbmono.EF.Models;

namespace Gbmono.Api.Models
{
    /// <summary>
    /// only shows simplified information when return product list result
    /// </summary>
    public class ProductSimpleModel
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int BrandId { get; set; }

        public string BrandName { get; set; }

        public double Price { get; set; }

        public string PrimaryImageUrl { get; set; }

        public List<Retailer> Retailers { get; set; }
    }  


    /// <summary>
    ///  detailed product model
    /// </summary>
    public class ProductDetailModel: ProductSimpleModel
    {
        // reuse mandatory properties
        // add extra properties 
        public string ProductCode { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
        public string CountryName { get; set; }
        public string Flavor { get; set; }
        public string Content { get; set; }
        public string WeightString { get; set; }
        public string Shape { get; set; }
        public string Texture { get; set; }
        public string BarCode { get; set; }
        public string Description { get; set; }
        public string Instruction { get; set; }        
        public string DescriptionImageUrl { get; set; }
        public string InstructionImageUrl { get; set; }

        public List<Category> Categories { get; set; }        
        public List<WebShop> WebShops { get; set; }
    }
}