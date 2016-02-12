using System.Net.Http;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace Gbmono.Api.ExceptionHandling
{
    public class GenericExceptionLogger: ExceptionLogger
    {
        private const string HttpContextBaseKey = "MS_HttpContext";

        // override the default log ,insert excetpion information into db
        public override void Log(ExceptionLoggerContext context)
        {
            // Retrieve the current HttpContext instance for this request.
            HttpContext httpContext = GetHttpContext(context.Request);

            if (httpContext == null)
            {
                return;
            }

            // get base excetpion
            var baseException = context.Exception.GetBaseException();

            // todo: Raphel
            // create logger instance here
            // log the error message and stack trace
            Utils.Logger.log.Error("RequestUri:" + context.Request.RequestUri + " Content:" + context.Request.Content.ReadAsStringAsync().Result + " /n Exception:" + context.ExceptionContext.Exception.ToString());
        }

        // extract the HttpContext from request
        private static HttpContext GetHttpContext(HttpRequestMessage request)
        {
            // get HttpContextBase
            HttpContextBase contextBase = GetHttpContextBase(request);

            if (contextBase == null)
            {
                return null;
            }

            // HttpContext instance
            return contextBase.ApplicationInstance.Context;
        }

        // extract the HttpContextBase from request
        private static HttpContextBase GetHttpContextBase(HttpRequestMessage request)
        {
            if (request == null)
            {
                return null;
            }

            object value;

            if (!request.Properties.TryGetValue(HttpContextBaseKey, out value))
            {
                return null;
            }

            return value as HttpContextBase;
        }
    }
}