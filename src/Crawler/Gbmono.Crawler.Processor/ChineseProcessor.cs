using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gbmono.Utils.Chinese;


namespace Gbmono.Crawler.Processor
{
    public class ChineseProcessor
    {
        public void Test()
        {
            var testList = new List<string>() { "東京", "東京喰種トーキョーグール" };

            foreach (var test in testList)
            {
                var result = ChineseConverter.ToSimplifiedChinese(test);
            }
            
        }


    }
}
