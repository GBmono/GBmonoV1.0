﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Gbmono.Api.Models;
using Gbmono.EF.Models;
using Gbmono.Search.IndexManager.Documents;

namespace Gbmono.Api.Extensions
{
    public static class ProductExtension
    {
        private static readonly string _noImage = "No_Image.png";

        public static ProductSimpleModel ToSimpleModel(this Product model)
        {
            return new ProductSimpleModel
            {
                ProductId = model.ProductId,
                PrimaryName = model.PrimaryName,
                SecondaryName = model.SecondaryName,
                BrandId = model.BrandId,
                BrandName = model.Brand.Name,
                Price = model.Price,
                Discount = model.Discount,
                ImgUrl = GetPrimaryImgUrl(model.Images) // get the default product image
            };
        }

        public static ProductSimpleModel ToSimpleModel(this ProductDoc doc)
        {
            return new ProductSimpleModel
            {
                ProductId = doc.ProductId,
                PrimaryName = string.Empty,
                SecondaryName = doc.Name,
                BrandId = doc.BrandId,
                BrandName = string.Empty,
                Price = doc.Price,
                Discount = doc.Discount,
                ImgUrl = GetPrimaryImgUrl(doc.Images)
            };
        }

        private static string GetPrimaryImgUrl(IEnumerable<ProductImage> images)
        {
            // no images
            if(images == null)
            {
                // return no-pic image
                return _noImage;
            }

            // no product images
            var productImg = images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product);
            if (productImg == null)
            {
                return _noImage;
            }

            // return product img url
            return productImg.FileName;
        }

        private static string GetPrimaryImgUrl(IEnumerable<ProductImageDoc> images)
        {
            // no images
            if (images == null)
            {
                // return no-pic image
                return _noImage;
            }

            // no product images
            var productImg = images.FirstOrDefault(s => s.ProductImageTypeId == (short)ProductImageType.Product);
            if (productImg == null)
            {
                return _noImage;
            }

            // return product img url
            return productImg.FileName;
        }
    }
}