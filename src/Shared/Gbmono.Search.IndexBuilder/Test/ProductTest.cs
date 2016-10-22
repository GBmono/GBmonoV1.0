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
                Data = new ProductSearchRequest { Keyword = "淡雅" },
                PageNumber = 1,
                PageSize = 10
            };
            var result = helper.SearchByKeyword(request);
        }
    }
}
