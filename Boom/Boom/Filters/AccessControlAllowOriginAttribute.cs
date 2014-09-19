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
            var headers = context.HttpContext.Response.Headers;
            if (!headers.ContainsKey("Access-Control-Allow-Origin"))
            {
                headers.Add("Access-Control-Allow-Origin", origin);
            }
        }
    }
}