using Microsoft.AspNet.Mvc;
using System;

namespace Boom
{
    /// <summary>
    /// Summary description for AccessControlAllowOriginAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class AccessControlAllowHeadersAttribute : ActionFilterAttribute
    {
        private readonly string[] origin;

	    public AccessControlAllowHeadersAttribute(params string[] origin)
	    {
            this.origin = origin;
	    }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            var headers = context.HttpContext.Response.Headers;
            if (!headers.ContainsKey("Access-Control-Allow-Headers"))
            {
                headers.Add("Access-Control-Allow-Headers", origin);
            }
        }
    }
}