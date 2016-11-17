using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager.IndexHelper
{
    public class SearchHistoryIndexHelper
    {
        private NestClient<SearchHistoryDoc> Client
        {
            get
            {
                return new NestClient<SearchHistoryDoc>().SetIndex(Constants.IndexName.GbmonoV1_searchhistory).SetType(Constants.TypeName.SearchHistory);
            }
        }

        public async Task IndexDoc(SearchHistoryDoc doc)
        {
            await Task.Run(() =>
            {
                Client.IndexDocument(doc);
            });            
        }
    }
}
