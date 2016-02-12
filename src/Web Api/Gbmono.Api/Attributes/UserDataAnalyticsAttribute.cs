using System;
using System.Net.Http;
using System.Web;
using System.Web.Http.Filters;

namespace Gbmono.Api.Attributes
{
    /// <summary>
    /// track user related data
    /// </summary>
    public class UserDataAnalyticsAttribute: ActionFilterAttribute
    {
        // Called by the framework after the action method executes.
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            // todo: insert user action data
            // controller name
            string controllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;

            // action name                
            string actionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            // todo: how can we grab the user action data based on the controller name or action name

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}