using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace ManahostManager.Utils.Attributs
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class NullValidationAttribute : ActionFilterAttribute
    {
        private readonly Func<Dictionary<string, object>, bool> _validateFunc;

        public NullValidationAttribute() : this(args => args.ContainsValue(null))
        {
        }

        public NullValidationAttribute(Func<Dictionary<string, object>, bool> validateFuncCondition)
        {
            _validateFunc = validateFuncCondition;
        }

        public override void OnActionExecuting(HttpActionContext actionExecutedContext)
        {
            if (_validateFunc(actionExecutedContext.ActionArguments))
            {
                actionExecutedContext.Response = actionExecutedContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "The argument cannot be null");
            }
        }
    }
}