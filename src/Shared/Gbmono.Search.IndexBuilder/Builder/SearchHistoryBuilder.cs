using Gbmono.Search.IndexManager;
using Gbmono.Search.IndexManager.Documents;
using Gbmono.Search.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexBuilder.Builder
{
    public class SearchHistoryBuilder
    {
        private NestClient<SearchHistoryDoc> Client
        {
            get
            {
                return new NestClient<SearchHistoryDoc>().SetIndex(Constants.IndexName.GbmonoV1_searchhistory).SetType(Constants.TypeName.SearchHistory);
            }
        }

        public void CreateIndexMapping()
        {
            Client.CreateIndexWithAutoMapping();
        }

        public void DeleteIndex()
        {
            Console.WriteLine("Delete SearchHistory index");
            Client.DeleteIndex();
        }
    }
}
