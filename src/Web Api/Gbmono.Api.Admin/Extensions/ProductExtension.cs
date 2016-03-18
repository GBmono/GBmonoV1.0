using System;
using System.Collections.Generic;
using System.Linq;

using Gbmono.EF.Models;
using Gbmono.Api.Admin.Models;

namespace Gbmono.Api.Admin.Extensions
{
    public static class ProductExtension
    {
        public static ProductSimpleModel ToSimpleModel(this Product model)
        {
            return new ProductSimpleModel
            {
                ProductId = model.ProductId,
                ProductCode = model.ProductCode,
                BrandId = model.BrandId,
                BrandName = model.Brand.Name,
                PrimaryName = model.PrimaryName,
                BarCode = model.BarCode,
                Price = model.Price,
                // Discount = model.Discount,
                ImgUrl = model.Images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product) == null
                                      ? ""
                                      : model.Images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product).FileName,
                CreatedDateTime = model.CreatedDate,
                ActivationDate = model.ActivationDate,
                ExpiryDate = model.ExpiryDate
            };
        }
    }
}