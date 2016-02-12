using Gbmono.Api.HttpResults;
using System.Web.Http.ExceptionHandling;

namespace Gbmono.Api.ExceptionHandling
{
    public class GenericExceptionHandler: ExceptionHandler
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