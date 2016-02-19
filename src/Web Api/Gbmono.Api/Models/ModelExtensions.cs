using System;
using System.Collections.Generic;
using System.Linq;

using Gbmono.EF.Models;

namespace Gbmono.Api.Models
{
    public static class ModelExtensions
    {
        public static ProductSimpleModel ToSimpleModel(this Product po)
        {
            var model = new ProductSimpleModel
            {
                ProductId = po.ProductId,
                ProductName = po.PrimaryName,
                BrandId = po.BrandId,
                BrandName = po.Brand.Name,
                Price = po.Price,
                PrimaryImageUrl = "content/images/demo/product_1.jpg"
                //Retailers = po.Retailers.ToList()
            };

            return model;           
        }

        //public static ProductDetailModel ToModel(this Product po)
        //{
        //    var r = new Random();
        //    var num = r.Next(3);
        //    var model = new ProductDetailModel
        //    {
        //        ProductId = po.ProductId,
        //        ProductName = po.PrimaryName,
        //        BrandId = po.BrandId,
        //        BrandName = po.Brand.Name,
        //        Price = po.Price,
        //        PrimaryImageUrl = string.Format("content/images/demo/product_{0}.png", num),
        //        ProductCode = po.ProductCode,
        //        ManufacturerId = po.Brand.ManufacturerId,
        //        ManufacturerName = po.Brand.Manufacturer.Name,
        //        CountryName = po.Country.Name,
        //        Flavor = po.Flavor,
        //        Content = po.Content,
        //        WeightString = string.Format("{0} {1}", po.Weight, po.WeightUnit),
        //        Shape = po.Shape,
        //        Texture = po.Texture,
        //        BarCode = po.BarCode,
        //        Description = po.Description,
        //        Instruction = po.Instruction,
        //        Retailers = po.Retailers.ToList(),
        //        WebShops = po.WebShops.ToList(),
        //        InstructionImageUrl = string.Format("content/images/demo/description_{0}.png", num)
        //    };
        //    return model;
        //}
    }
}