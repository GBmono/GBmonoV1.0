using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

namespace Gbmono.Api.Attributes
{
    /// <summary>
    /// track user related data
    /// </summary>
    public class PageEventTrackAttribute: ActionFilterAttribute
    {
        private readonly string _attribute;

        // ctor with name parameter
        public PageEventTrackAttribute(string name)
        {
            _attribute = name;
        }

        public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {

            return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
        }
        //public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        //{
        //    // todo: insert user action data
        //    // controller name
        //    string controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;

        //    // action name                
        //    string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

        //    // todo: how can we grab the user action data based on the controller name or action name

        //    base.OnActionExecuted(actionExecutedContext);
        //}
    }
}