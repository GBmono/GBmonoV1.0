using Gbmono.Search.IndexBuilder.Builder;
using Gbmono.Search.IndexBuilder.Test;
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
            //retail shop builder
            //var builder = new RetailerShopsBuilder();
            //builder.Build();

            //retail shop test
            RetailShopTest test = new RetailShopTest();
            test.GetRetailShopById(14);
        }
    }
}
