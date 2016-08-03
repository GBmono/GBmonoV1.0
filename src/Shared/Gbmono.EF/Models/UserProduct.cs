using System;

namespace Gbmono.EF.Models
{
    public class UserProduct
    {
        public int UserProductId {get;set;}

        public string UserId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime Created { get; set; }
    }
}
