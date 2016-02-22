﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Gbmono.Api.Models;
using Gbmono.EF.Models;

namespace Gbmono.Api.Extensions
{
    public static class ProductExtension
    {
        public static ProductSimpleModel ToSimpleModel(this Product model)
        {
            return new ProductSimpleModel
            {
                ProductId = model.ProductId,
                PrimaryName = model.PrimaryName,
                BrandId = model.BrandId,
                BrandName = model.Brand.Name,
                Price = model.Price,
                Discount = model.Discount
            };
        }
    }
}