using Microsoft.AspNet.Mvc;
using System;

namespace Boom
{
    /// <summary>
    /// Sets the "Content-Type: application/json" header to the response.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class ApplicationJsonHeader : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            context.HttpContext.Response.Headers.Add("Content-Type", new string[] { "application/json" });
        }
    }
}