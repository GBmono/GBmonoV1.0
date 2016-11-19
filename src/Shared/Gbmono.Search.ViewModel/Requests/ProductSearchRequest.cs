using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.ViewModel.Requests
{
    public class ProductSearchRequest : SearchKeyword
    {
        public bool NeedAggregation { get; set; } = true;
        public List<string> BrandName { get; set; } = new List<string>();
        //public int CategoryLevel { get; set; }
        public List<string> CategoryName { get; set; } = new List<string>();

        public List<string> Tag { get; set; } = new List<string>();


        //public int? FilterCategoryLevel { get; set; }
    }
}
