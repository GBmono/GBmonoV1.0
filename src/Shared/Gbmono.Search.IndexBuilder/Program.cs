using Gbmono.Search.IndexBuilder.Builder;
using Gbmono.Search.IndexBuilder.Test;
using Gbmono.Search.IndexManager.SearchHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            //search history builder
            //var builder = new SearchHistoryBuilder();
            //builder.DeleteIndex();
            //builder.CreateIndexMapping();

            //search history test
            var test = new SearchHistoryHelper();
            test.SearchByPrefixKeyword("祛");

            //retail shop builder
            //var builder = new RetailerShopsBuilder();
            //builder.CreateIndexMapping();
            //builder.Build();

            //retail shop test
            //RetailShopTest test = new RetailShopTest();
            //test.GetRetailShopById();
            //test.GetRetailShopByCity();
            //test.GetRetailShopByKeyword();

            //product builder
            //var builder = new ProductBuilder();
            //builder.DeleteIndex();
            //builder.CreateIndexMapping();
            //builder.Build();

            //product test
            //var test = new ProductTest();
            //test.GetProductByKeyword();
            //test.GetProductByPrefixKeyword();

            ////product tag builder
            //var builder = new ProductTagBuilder();
            //builder.DeleteIndex();
            //builder.CreateIndexMapping();
            //builder.Build();

            //product tag test
            //var test = new ProductTagTest();
            //test.GetProductTagByKeyword();
            //test.GetPrefixProductTagByKeyword();
        }
    }
}
