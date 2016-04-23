using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.ViewModel.Requests
{
    public class RetailShopSearchRequest
    {
        public int? RetailerId { get; set; }
        public string Keyword { get; set; }
    }
}
