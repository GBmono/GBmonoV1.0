using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Gbmono.Api.HttpResults
{
    public class DataInvalidResult : IHttpActionResult
    {
        public string Content { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        // ctor
        public DataInvalidResult(string content, HttpRequestMessage request)
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            Content = content;
            Request = Request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Conflict)
            {
                Content = new StringContent(Content, Encoding.UTF8),
                RequestMessage = Request
            };

            return Task.FromResult(response);
        }
    }
}