using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager
{
    public class EsRequestException : Exception
    {
        public int HttpState { get; set; }

        public string RawResponse { get; set; }

        public bool IsTimeOut { get; set; }

        public bool IsSuccess { get; set; }

        public bool Isvalid { get; set; }

        public EsRequestException(string msg) : base(msg)
        {
        }
    }
}
