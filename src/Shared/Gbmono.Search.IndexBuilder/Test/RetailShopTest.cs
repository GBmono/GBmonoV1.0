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
    public class RetailShopTest
    {
        private RetailShopHelper helper;
        public RetailShopTest()
        {
            helper = new RetailShopHelper();
        }

        public void GetRetailShopById()
        {
            var retailshopId = 14;
            var doc = helper.GetRetailShopDocById(retailshopId);

            Console.WriteLine("retail name :{0}", doc.Name);
            Console.WriteLine("retail display name :{0}", doc.DisplayName);
            Console.WriteLine("retail address :{0}", doc.Address);
            Console.WriteLine("retail open time :{0}", doc.OpenTime);
            Console.WriteLine("retail close day :{0}", doc.CloseDay);
            Console.WriteLine("retail phone :{0}", doc.Phone);
            Console.WriteLine("retail latitude :{0}", doc.Latitude);
            Console.WriteLine("retail longitude :{0}", doc.Longitude);
        }

        public void GetRetailShopByCity()
        {
            var cityId = 2;
            var retailId = 1;
            var result = helper.GetRetailShopDocByCity(cityId, retailId);
        }

        public void GetRetailShopByKeyword()
        {
            var request = new PagedRequest<RetailShopSearchRequest>
            {
                Data = new RetailShopSearchRequest { Keyword = "東京" },
                PageNumber = 2,
                PageSize = 10
            };
            var result = helper.SearchByKeyword(request);
        }
    }
}
