using Gbmono.Search.Utils;
using Gbmono.Search.Utils.Extentions;
using Gbmono.Search.ViewModel;
using Nest;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager
{
    public class NestClient<T> where T : class
    {
        private string _nodeUrl = ConfigHelper.GetSetting(Constants.AppSettingsKeys.ElasticNodeUrlKey, "http://localhost:9200");

        private string _indexName = string.Empty;
        private string _typeName = null;
        private string _routeName = null;
        private int _pageSize = 10;
        private int _pageNumber = 1;

        private static bool? _httpCompressed;

        public bool HttpComppressed
        {
            get
            {
                if (!_httpCompressed.HasValue)
                {
                    var settingVal = ConfigHelper.GetSetting(Constants.AppSettingsKeys.ElasticHttpCompressed, "no");

                    _httpCompressed = settingVal.Trim().ToLower() == "yes";
                }

                return _httpCompressed.Value;
            }
        }

        private Func<FieldsDescriptor<T>, IPromise<Fields>> _excludedFields;

        private Func<SortDescriptor<T>, IPromise<IList<ISort>>> _sortSelector;

        private ElasticClient _client;

        private ConnectionSettings Conn
        {
            get
            {
                var node = new Uri(_nodeUrl);
                var conn = new ConnectionSettings(node).DisableDirectStreaming(true);
                conn.EnableHttpCompression(HttpComppressed);
                return conn;
            }
        }

        protected ElasticClient EsClient
        {
            get
            {
                if (_client == null)
                {
                    _client = new ElasticClient(Conn);
                }
                return _client;
            }
        }

        public ElasticClientProxy Client
        {
            get
            {
                return new ElasticClientProxy(EsClient, Conn);
            }
        }

        public NestClient(string urlKey = null)
        {
            urlKey = string.IsNullOrWhiteSpace(urlKey) ? Constants.AppSettingsKeys.ElasticNodeUrlKey : urlKey;
            _nodeUrl = ConfigHelper.GetSetting(urlKey, "http://localhost:9200");
        }

        #region Bilder Pattern

        public NestClient<T> SetIndex(string indexName)
        {
            _indexName = indexName;
            return this;
        }

        public NestClient<T> SetType(string typeName)
        {
            _typeName = typeName;
            return this;
        }

        public NestClient<T> SetPageSize(int pageSize)
        {
            if (pageSize >= 0)
            {
                _pageSize = pageSize;
            }

            return this;
        }

        public NestClient<T> SetPageNum(int pageNum)
        {
            if (pageNum > 0)
            {
                _pageNumber = pageNum;
            }
            return this;
        }
        #endregion

        public void IndexDocuments(IEnumerable<T> docs)
        {
            if (docs == null || !docs.Any())
            {
                throw new ArgumentException("docs");
            }

            var response = Client.IndexMany(docs, _indexName, _typeName);
        }

        public async Task IndexDocumentsAsync(IEnumerable<T> docs, Func<T, string> failMsg = null)
        {
            if (docs.IsNullOrEmpty())
            {
                throw new ArgumentException("docs");
            }

            var task = Client.IndexManyAsync(docs, _indexName, _typeName).ContinueWith((t) =>
            {
                if (t.Result.ServerError != null)
                {
                    Console.WriteLine(t.Result.ServerError.Error);

                    if (failMsg != null)
                    {
                        Console.WriteLine(failMsg);
                    }
                }
            });


        }

        public void IndexDocument(T doc, Func<T, string> failMsg = null, string routingValue = null)
        {
            if (doc == null)
            {
                throw new ArgumentException("doc");
            }
            IIndexResponse response;
            if (string.IsNullOrEmpty(routingValue))
            {
                response = Client.Index(doc,
                    i => i.Index(_indexName).Type(_typeName));
            }
            else
            {
                response = Client.Index(doc,
                    i => i.Index(_indexName).Type(_typeName).Routing(routingValue));
            }


            if (response.ServerError != null)
            {
                //output fail message
                if (failMsg != null)
                {
                    Console.WriteLine(failMsg(doc));
                }

                Console.WriteLine("Error:{0}", response.ServerError.Error);
                Console.WriteLine("Exception:{0}", response.ServerError.Error.Type);
            }
        }

        public void BulkIndexDocument(IEnumerable<T> docs)
        {
            if (null == docs || docs.Count() == 0)
            {
                throw new ArgumentException("docs");
            }

            var descriptor = new BulkDescriptor();
            foreach (var doc in docs)
            {
                descriptor.Index<T>(i => i.Index(_indexName).Type(_typeName).Document(doc));
            }
            var response = Client.Bulk(d => descriptor);

            if (response.ServerError != null)
            {
                Console.WriteLine("Error:{0}", response.ServerError.Error);
                Console.WriteLine("Exception:{0}", response.ServerError.Error.Type);
            }
        }

        public void BulkIndexRoutedDocument(IEnumerable<Models.RouteGeneric<T>> routeNamedDocs)
        {
            if (null == routeNamedDocs || !routeNamedDocs.Any())
            {
                throw new ArgumentException("docs");
            }

            var descriptor = new BulkDescriptor();
            foreach (var routedDoc in routeNamedDocs)
            {
                Func<BulkIndexDescriptor<T>, BulkIndexDescriptor<T>> selector = i => i.Index(_indexName).Type(_typeName).Document(routedDoc.Doc);

                if (!string.IsNullOrWhiteSpace(routedDoc.RouteName))
                {
                    selector += i => i.Routing(routedDoc.RouteName);
                }

                descriptor.Index<T>(selector);
            }
            var response = Client.Bulk(d => descriptor);

            if (response.ServerError != null)
            {
                Console.WriteLine("Error:{0}", response.ServerError.Error.CausedBy);
                Console.WriteLine("Exception:{0}", response.ServerError.Error.Type);
            }
        }

        public void BulkIndexDocument(ConcurrentDictionary<T, string> dict)
        {
            if (null == dict || dict.Count == 0)
            {
                throw new ArgumentException("dict");
            }

            var descriptor = new BulkDescriptor();
            Parallel.ForEach(dict, row =>
            {
                descriptor.Index<T>(i => i.Index(_indexName).Type(_typeName).Routing(row.Value).Document(row.Key));
            });
            var response = Client.Bulk(d => descriptor);
            if (response.ServerError != null)
            {
                Console.WriteLine("Error: {0}", response.ServerError.Error);
                Console.WriteLine("Exception: {0}", response.ServerError.Error.Type);
            }
        }

        public void DeleteDocument(string id)
        {
            Client.Delete<T>(id, d => d.Index(_indexName).Type(_typeName));
        }

        public void DeleteDocument(long id)
        {
            Client.Delete<T>(id, d => d.Index(_indexName).Type(_typeName));
        }

        public ISearchResponse<T> SearchResponse(IQueryContainer query = null, IQueryContainer filter = null, Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregation = null)
        {
            int skip = _pageSize * (_pageNumber - 1);

            skip = Math.Max(0, skip);

            Func<SearchDescriptor<T>, SearchDescriptor<T>> descriptor =
                i => i.Index(_indexName)
                      .Type(_typeName)
                      .Skip(skip)
                      .Size(_pageSize);

            if (_excludedFields != null)
            {
                descriptor += i => i.Source(s => s.Exclude(_excludedFields));
            }
            if (query != null || filter != null)
            {
                var mainQuery = new QueryContainer();

                if (query != null)
                {
                    mainQuery &= (QueryContainer)query;
                }
                if (filter != null)
                {
                    mainQuery &= new BoolQuery() { Filter = new[] { (QueryContainer)filter } };
                }
                descriptor += i => i.Query(q => mainQuery);
            }

            if (aggregation != null)
            {
                descriptor += i => i.Aggregations(aggregation);
            }
            if (_sortSelector != null)
            {
                descriptor += i => i.Sort(_sortSelector);
            }
            var response = Client.Search<T>(descriptor);

            return response;
        }

        public PagedResponse<T> PostSearch(IQueryContainer query = null, IQueryContainer filter = null, Func<AggregationContainerDescriptor<T>, IAggregationContainer> aggregation = null)
        {
            var response = this.SearchResponse(query, filter, aggregation);

            return WrapResult(response);
        }        

        public PagedResponse<T> PostSearch(QueryContainer query, QueryContainer filter = null)
        {
            var response = SearchResponse(query, filter);

            return WrapResult(response);
        }

        public void DeleteIndex()
        {
            if (string.IsNullOrEmpty(_indexName))
            {
                throw new ArgumentException("_indexName");
            }
            var response = Client.DeleteIndex(_indexName);
            if (response.ServerError != null)
            {
                Console.WriteLine(response.ServerError.Error);
                Console.WriteLine(response.ServerError.Error.Type);
            }
        }

        public void CreateIndexWithMappingForProduct()
        {
            //var response=Client.CreateIndex(d=> d
            //.Index(_indexName)
            //.AddMapping<Product>
        }

        public PagedResponse<T> WrapResult(ISearchResponse<T> response)
        {
            var result = new PagedResponse<T>();

            result.Data = response.Documents;
            result.Score = response.Hits.Select(i => i.Score).ToArray();

            result.Total = response.Total;
            result.PageSize = this._pageSize;
            result.PageNumber = this._pageNumber;

            result.Aggregation = response.Aggs;


            return result;
        }
    }
}
