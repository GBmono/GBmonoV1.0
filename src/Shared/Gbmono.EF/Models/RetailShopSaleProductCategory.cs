using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.Models
{
    public class RetailShopSaleProductCategory
    {
        public int RetailShopSaleProductCategoryId { set; get; }


        public int RetailShopId { set; get; }
        public RetailShop RetailShop { get; set; }

        public int SaleProductCategoryId { set; get; }
        public SaleProductCategory SaleProductCategory { set; get; }
    }
}
