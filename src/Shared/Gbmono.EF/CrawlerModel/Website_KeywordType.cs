using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.CrawlerModel
{
    public class Website_KeywordType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int WebSiteId { get; set; }
        public int KeyWordTypeId { get; set; }
        public string XPathScript { get; set; }
        public bool IsMultiple { get; set; }
        public bool IsAttribute { get; set; }
        public string AttributeName { get; set; }
        public bool IsSplitString { get; set; }
        public string SplitKey { get; set; }
        public bool NeedDownloadFile { get; set; }

        ////[ForeignKey("KeyWordTypeId")]
        //public virtual KeywordType KeywordType { get; set; }
        ////[ForeignKey("WebSiteId")]
        //public virtual WebsiteName WebsiteName { get; set; }

    }
}
