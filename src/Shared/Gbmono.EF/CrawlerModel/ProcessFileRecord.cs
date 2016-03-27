using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.CrawlerModel
{
    public class ProcessFileRecord
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int ProductInfoId { get; set; }
        public int FileId { get; set; }

        //[Required]
        //[ForeignKey("ProductInfoId")]
        //public virtual ProductInfo ProductInfo { get; set; }

        //[Required]
        //[ForeignKey("FileId")]
        //public virtual CrawlHistory CrawlHistory { get; set; }


    }
}
