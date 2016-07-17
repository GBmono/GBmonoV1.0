using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.Models
{
    public class NArticleImage
    {
        public int NArticleImageId { set; get; }

        public int NArticleId { set; get; }

        public string Url { set; get; }
    }
}
