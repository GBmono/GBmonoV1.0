using System;

namespace Gbmono.Utils.Chinese
{
    public class ChineseConverter
    {
        public static string ToSimplifiedChinese(string str)
        {
            return Microsoft.VisualBasic.Strings.StrConv(str, Microsoft.VisualBasic.VbStrConv.SimplifiedChinese, 0);
        }

        public static string ToTraditionalChinese(string str)
        {
            return Microsoft.VisualBasic.Strings.StrConv(str, Microsoft.VisualBasic.VbStrConv.TraditionalChinese, 0);
        }
    }
}
