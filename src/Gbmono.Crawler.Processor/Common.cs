using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Gbmono.Crawler.Processor
{
    public static class Common
    {

        public static string StripHtml(this string originalHtml)
        {
            return Regex.Replace(originalHtml, @"<(.|\n)*?>",
                string.Empty).Replace("\n", string.Empty);
        }
    }
}
