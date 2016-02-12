using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gbmono.Search.IndexManager
{
    public class ElasticClientProxy : ElasticClient
    {
        private ElasticClient _client;

        public ElasticClientProxy(ElasticClient client, IConnectionSettingsValues settings) : base(settings)
        {
            _client = client;
        }


        public EsRequestException CheckResponse<T>(ISearchResponse<T> response) where T : class
        {
            EsRequestException exception = null;
            if (response.ConnectionStatus.HttpStatusCode > 200)
            {
                #region Generate Exception

                var msg = "";
                if (response.ServerError != null)
                {
                    msg = response.ServerError.Error;
                }

                exception = new EsRequestException(msg)
                {
                    IsTimeOut = response.TimedOut,
                    Isvalid = response.IsValid,
                    IsSuccess = response.ConnectionStatus.Success
                };

                if (null != response.ConnectionStatus.ResponseRaw)
                {
                    exception.RawResponse = Encoding.Default.GetString(response.ConnectionStatus.ResponseRaw);
                }
                #endregion


            }

            return exception;
        }

        public ISearchResponse<T> Search<T>(Func<SearchDescriptor<T>, SearchDescriptor<T>> searchSelector) where T : class
        {
            var response = _client.Search(searchSelector);

            //Console.WriteLine(response.ElapsedMilliseconds);

            var exception = CheckResponse(response);
            if (exception != null)
            {
                throw exception;
            }

            return response;
        }


        public ISearchResponse<T> Search<T>(ISearchRequest request) where T : class
        {
            var response = _client.Search<T>(request);

            var exception = CheckResponse(response);
            if (exception != null)
            {
                throw exception;
            }

            return response;
        }

    }
}
