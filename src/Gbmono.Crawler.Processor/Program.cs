using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Crawler.Processor
{
    class Program
    {
        static void Main(string[] args)
        {
            //var processor = new Processor();
            //processor.Process();

            //var p = new ProductMap();
            //p.Mapping();

            var processor = new StoreProcessor();
            processor.Process();

        }
    }
}
