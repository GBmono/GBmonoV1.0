﻿using System;
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

            //var processor = new StoreProcessor();
            //processor.Process();

            //var processor = new StoreMapping();
            //processor.Mapping();

            //var processor = new ProductTagRandomAssign();
            //processor.RandomMapping();

            //Chinese Tester
            //var processor = new ChineseProcessor();
            //processor.Test();

            //var processor = new ArticleProcessor();
            //processor.Process();

            var process = new ArticleHuffingtonProcess();
            process.Process();

        }
    }
}
