using System;
using System.Collections.Generic;


namespace Gbmono.Common
{
    public static class StringHelper
    {
        // 删除空格，换行字符
        public static string RemoveEmptyOrWrapCharacters(this string origin)
        {
            return origin.Replace("\r", "").Replace("\n", "").Trim().ToString();
        }

        // 转半角函数
        public static string ToDBC(this string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        //转换excel datetime类型
        public static DateTime ToExcelDatetime(this string origin)
        {
            return DateTime.FromOADate(double.Parse(origin));
        }
    }
}
