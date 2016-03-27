using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.CrawlerModel
{
    public class CrawlHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [StringLength(1024)]
        public string Key { set; get; }

        public int GroupId { set; get; }

    }
}
