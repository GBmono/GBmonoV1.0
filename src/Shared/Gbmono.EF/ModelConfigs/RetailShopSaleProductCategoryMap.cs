using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.EF.Models;

namespace Gbmono.EF.ModelConfigs
{
    public class RetailShopSaleProductCategoryMap : EntityTypeConfiguration<RetailShopSaleProductCategory>
    {
        public RetailShopSaleProductCategoryMap()
        {
            ToTable("RetailShopSaleProductCategory");

            HasKey(m => m.RetailShopSaleProductCategoryId);
        }

    }

}
