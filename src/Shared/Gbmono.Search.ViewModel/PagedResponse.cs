using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.ViewModel
{
    public class PagedResponse<T> : Pager where T : class
    {
        public IEnumerable<T> Data { get; set; }

        public double[] Score { get; set; }

        public AggregationsHelper Aggregation { set; get; }
    }
}
