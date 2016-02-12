using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gbmono.Api.Utils
{
    public class Logger
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
    }
}