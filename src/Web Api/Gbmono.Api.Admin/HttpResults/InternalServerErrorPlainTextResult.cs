using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Gbmono.Api.Admin.HttpResults
{
    /// <summary>
    /// return internal server error plain text result
    /// </summary>
    public class InternalServerErrorPlainTextResult : IHttpActionResult
    {
        // properties with private set
        public string Content { get; private set; }
        public HttpRequestMessage Request { get; private set; }

        // ctor
        public InternalServerErrorPlainTextResult(string content, HttpRequestMessage request)
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
            Request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            // return http response with content and encoding
            var response = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent(Content, Encoding.UTF8),
                RequestMessage = Request
            };

            return Task.FromResult(response);
        }
    }
}