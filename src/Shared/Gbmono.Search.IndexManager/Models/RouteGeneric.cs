using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Models
{
    public class RouteGeneric<T> where T : class
    {
        public T Doc { get; set; }

        public string RouteName { get; set; }
    }
}
