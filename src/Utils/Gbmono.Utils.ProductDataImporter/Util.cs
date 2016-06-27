using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Utils.ProductDataImporter
{
    public static class Util
    {
        public static DateTime From1900(this string days)
        {
            try
            {
                return new DateTime(1900, 1, 1).AddDays(double.Parse(days) - 2);
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        public static string IndexAppend(string column, int index)
        {
            return string.Format("{0}{1}", column, index);
        }

    }
}
