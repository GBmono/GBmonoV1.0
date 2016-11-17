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
    public class ProductTagTest
    {
        private ProductTagHelper helper;
        public ProductTagTest()
        {
            helper = new ProductTagHelper();
        }

        public void GetProductTagByKeyword()
        {
            var request = new PagedRequest<ProductTagSearchRequest>
            {
                Data = new ProductTagSearchRequest { Keyword = "肉体疲労 疲労" },
                PageNumber = 1,
                PageSize = 10
            };
            var result = helper.SearchByKeyword(request);
        }

        public void GetPrefixProductTagByKeyword()
        {
            var request = new PagedRequest<ProductTagSearchRequest>
            {
                Data = new ProductTagSearchRequest { Keyword = "肉" },
                PageNumber = 1,
                PageSize = 10
            };
            var result = helper.SearchByPrefixKeyword(request);
        }
    }
}
