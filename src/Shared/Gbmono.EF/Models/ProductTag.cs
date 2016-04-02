﻿using System;

namespace Gbmono.EF.Models
{
    public class ProductTag
    {
        public int ProductTagId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
