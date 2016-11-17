using Gbmono.Search.IndexManager.SearchHelper;
using Gbmono.Search.ViewModel;
using Gbmono.Search.ViewModel.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexBuilder.Test
{
    public class ProductTest
    {
        private ProductHelper helper;
        public ProductTest()
        {
            helper = new ProductHelper();
        }

        public void GetProductByKeyword()
        {
            var request = new PagedRequest<ProductSearchRequest>
            {
                Data = new ProductSearchRequest { Keyword = "肤质 祛痘", BrandName = new List<string> { "露适她株式会社","B by E corporation" }, CategoryName = new List<string> { "洁面", "基础化妆品" } },//, CategoryName = "基础化妆类", FilterCategoryLevel = 2
                PageNumber = 1,
                PageSize = 10,
            };
            var result = helper.SearchByKeyword(request);
        }

        public void GetProductByPrefixKeyword()
        {
            var request = new ProductSearchRequest
            {
                Keyword = "MAMA"
            };
            var result = helper.SearchByPrefixKeyword(request);
        }
    }
}
