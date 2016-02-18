﻿using System;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 商品统计模型 (包括产品点击测试，被扫描次数等)
    /// </summary>
    public class ProductStats
    {
        public int ProductStatsId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public short StatsType { get; set; }

        public string UserId { get; set; }

        public string CreatedDateTime { get; set; } 
    }
}
