using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.ViewModel
{
    public class PagedRequest<T> : Pager where T : class
    {
        public T Data { get; set; }

        public bool IsTermsReload { get; set; }

        public bool RankScore { get; set; }
    }
}
