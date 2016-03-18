using System;

namespace Gbmono.EF.Models
{
    /// <summary>
    /// 商品统计模型 (包括产品点击测试，被扫描次数等)
    /// </summary>
    public class ProductEvent
    {
        public int ProductEventId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public short EventTypeId { get; set; }

        public string UserName { get; set; }

        public DateTime Created { get; set; } 
    }
}
