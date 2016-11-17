using Gbmono.Search.Utils;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.Documents
{
    [ElasticsearchType(Name = Constants.TypeName.SearchHistory)]
    public class SearchHistoryDoc
    {
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string UserName { get; set; }
        [String(Index = FieldIndexOption.NotAnalyzed)]
        public string Keyword { get; set; }
        public SearchType SearchType { get; set; }
        public DateTimeOffset SearchTime { get; set; } = DateTimeOffset.Now;
    }

    public enum SearchType
    {
        product,
        brand,
        article,
        retailshop
    }
}
