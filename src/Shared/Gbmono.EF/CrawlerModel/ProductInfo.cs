using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.CrawlerModel
{
    public class ProductInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int SiteId { get; set; }
        public string Url { get; set; }
        public System.DateTime CreateTime { get; set; }

        //public virtual ProcessFileRecord ProcessFileRecord { get; set; }
        ////[ForeignKey("SiteId")]
        //public virtual WebsiteName WebsiteName { get; set; }
    }
}
