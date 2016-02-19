using System;
using System.Collections.Generic;


namespace Gbmono.Common
{
    public static class Validator
    {
        /// <summary>
        /// full product code is 10 digits number
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static bool IsValidFullProductCode(string code)
        {
            if (string.IsNullOrEmpty(code) || code.Length != 10)
            {
                return false;
            }

            // todo: add more validation check

            return true;
        }
    }
}
