using System;
using System.Collections.Generic;

namespace Gbmono.EF.Models
{
    public class UserFavorite
    {
        public string UserId { get; set; }

        public int ProductId { get; set; }

        public DateTime DateTime { get; set; }
    }
}
