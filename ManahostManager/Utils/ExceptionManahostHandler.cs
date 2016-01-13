using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;

namespace ManahostManager.Utils
{
    internal class ErrorMsg
    {
        public ErrorMsg(string msg)
        {
            Error = "Une erreur est survenue, les administrateurs de manahost ont été prévenu." + msg;
        }

        public ErrorMsg() : this("")
        {
            
        }

        public string Error { get; set; }
    }

    public class ExceptionManahostHandler : ExceptionHandler
    {
        public override Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            Handle(context);
            return Task.FromResult(0);
        }

        public override void Handle(ExceptionHandlerContext context)
        {
            HttpRequestMessage request = context.Request;


            context.Result = new ResponseMessageResult(request.CreateResponse(HttpStatusCode.InternalServerError, new ErrorMsg(context.Exception.Message)));
        }

        public override bool ShouldHandle(ExceptionHandlerContext context)
        {
            return context.ExceptionContext.CatchBlock.IsTopLevel;
        }
    }
}