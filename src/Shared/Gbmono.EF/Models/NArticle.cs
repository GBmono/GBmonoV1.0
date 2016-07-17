using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.Models
{
    public class NArticle
    {
        public int NArticleId { get; set; }


        public string Title { get; set; }

        // html code
        public string Body { get; set; }

        public string CorverUrl { get; set; }

        public DateTime CreateDate { get; set; }


        public DateTime PublicshDate { get; set; }

        public string SourceUrl { set; get; }
    }
}
