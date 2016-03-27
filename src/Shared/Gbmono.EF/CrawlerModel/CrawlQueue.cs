using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.EF.CrawlerModel
{
    public class CrawlQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int GroupId { set; get; }

        [MaxLength]
        public byte[] SerializedData { set; get; }

        [MaxLength]
        public string Key { set; get; }

        public bool Exclusion { set; get; }
    }
}
