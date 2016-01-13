using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using ManahostManager.Model;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Utils
{

    internal class ModelStateHub
    {
        public ModelStateHub(ModelStateDictionary StateObject)
        {
            Message = "The request is invalid.";
            ModelState = new Dictionary<string, List<string>>();

            foreach (var item in StateObject)
            {
                List<string> NewListError = new List<string>();
                foreach(var itemString in item.Value.Errors)
                {
                    NewListError.Add(itemString.ErrorMessage);
                }
                ModelState.Add(item.Key, NewListError);
            }
        }

        public string Message { get; set; }

        public IDictionary<string, List<string>> ModelState { get; set; }
    }
    public class ManahostValidationExceptionHubPipeline : HubPipelineModule
    {
        public override Func<IHubIncomingInvokerContext, Task<object>> BuildIncoming(Func<IHubIncomingInvokerContext, Task<object>> invoke)
        {
            return async context =>
            {
                try
                {
                    // This is responsible for invoking every server-side Hub method in your SignalR app.
                    return await invoke(context);
                }
                catch (ManahostValidationException e)
                {
                    // If a Hub method throws, have it return the error message instead.
                    return new HubModel(400, new ModelStateHub(e.ModelState));
                }
            };
        }
    }
}