using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;

namespace ManahostManager.Utils
{
    public class ExceptionValidationHandler : ExceptionFilterAttribute
    {
        public override Task OnExceptionAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
        {
            OnException(actionExecutedContext);
            return Task.FromResult(0);
        }

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception is ManahostValidationException)
            {
                HttpRequestMessage RequestMessage = actionExecutedContext.Request;
                ManahostValidationException ExceptionManahost = actionExecutedContext.Exception as ManahostValidationException;

                if (ExceptionManahost.ModelState.Count != 0 && !ExceptionManahost.ModelState.IsValid)
                    actionExecutedContext.Response = RequestMessage.CreateErrorResponse(HttpStatusCode.BadRequest, ExceptionManahost.ModelState);
            }
        }
    }
}