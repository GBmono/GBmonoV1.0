using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    public class UserFavorite
    {
        public int UserFavoriteId { get; set; }

        public string UserId { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public DateTime Created { get; set; }
    }
}
