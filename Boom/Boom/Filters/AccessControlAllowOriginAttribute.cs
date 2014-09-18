using Microsoft.AspNet.Mvc;
using System;

namespace Boom
{
    /// <summary>
    /// Summary description for AccessControlAllowOriginAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AccessControlAllowOriginAttribute : ActionFilterAttribute
    {
        private readonly string[] origin;

        public AccessControlAllowOriginAttribute(params string[] origin)
        {
            this.origin = origin;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", origin);
        }
    }
}