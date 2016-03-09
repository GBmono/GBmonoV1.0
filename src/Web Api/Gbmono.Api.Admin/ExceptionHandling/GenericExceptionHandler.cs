using System.Web.Http.ExceptionHandling;
using Gbmono.Api.Admin.HttpResults;

namespace Gbmono.Api.Admin.ExceptionHandling
{
    /// <summary>
    /// overrides the default web api exception handler
    /// returns InternalServerError result with base exception message
    /// </summary>
    public class GenericExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            // get base exception
            var baseExp = context.Exception.GetBaseException();

            // set the result
            context.Result = new InternalServerErrorPlainTextResult(baseExp.Message, context.Request);
        }
    }
}