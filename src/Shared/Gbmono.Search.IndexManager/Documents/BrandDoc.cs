using Gbmono.Search.Utils;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Documents
{
    [ElasticsearchType(IdProperty ="BrandId",Name =Constants.TypeName.Brand)]
    public class BrandDoc
    {
        public int BrandId { get; set; }
        [String(Analyzer = "ik_max_word", SearchAnalyzer = "ik_max_word")]
        public string Name { get; set; }
        [String(Analyzer = "ik_max_word", SearchAnalyzer = "ik_max_word")]
        public string DisplayName { get; set; }
        public string BrandCode { get; set; }
        public string FirstCharacter { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }
    }
}
