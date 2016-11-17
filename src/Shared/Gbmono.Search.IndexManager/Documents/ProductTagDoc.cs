using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Documents
{
    public class ProductTagDoc
    {
        public int Id { get; set; }
        [String(Index =FieldIndexOption.NotAnalyzed)]
        public string Name_NA { get; set; }
        [String(Analyzer = "ik_max_word", SearchAnalyzer = "ik_max_word")]
        public string Name { get; set; }
        public int TagTypeId { get; set; }
    }
}
