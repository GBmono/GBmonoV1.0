using Gbmono.Search.IndexManager.Builders;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.SearchHelper
{
    public class SearchHistoryHelper
    {
        private NestClient<SearchHistoryHelper> Client
        {
            get
            {
                return new NestClient<SearchHistoryHelper>().SetIndex(Constants.IndexName.GbmonoV1_searchhistory).SetType(Constants.TypeName.SearchHistory);
            }
        }

        public List<string> SearchByPrefixKeyword(string keyword)
        {
            QueryContainer filter = null;
            var query = new QueryBuilder()
                .AndPrefixMatch("keyword", keyword)
                .Build();
            var categoryAgg = new AggregationContainerDescriptor<SearchHistoryDoc>()
                .Terms("agg_name", f => f.Field("keyword").Size(5));

            var searchResult = Client.SetPageNum(0).SetPageSize(10).SearchResponse(query, filter, a => categoryAgg);
            var doc = Client.WrapResult(searchResult);
            var agg = doc.Aggregation.Aggregations["agg_name"];
            var result = new List<string>();
            foreach (KeyedBucket item in ((BucketAggregate)agg).Items)
            {
                result.Add(item.Key);
            }
            return result;
        }
    }
}
