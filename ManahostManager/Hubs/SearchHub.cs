using ManahostManager.Controllers;
using ManahostManager.Model;
using ManahostManager.Services;
using ManahostManager.Utils;
using Microsoft.AspNet.SignalR;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Hubs
{
    public class SearchHub : AbstractHub
    {
        [Dependency]
        public ISearchService SearchService { get; set; }

        public object Post(AdvSearch search)
        {
            if (!AuthorizationCheck(GenericNames.REGISTERED_VIP))
            {
                return new HubModel(401, null);
            }
            ModelStateDictionary StateObject = new ModelStateDictionary();
            if (search == null)
            {
                StateObject.AddModelError("AdvSearch", GenericError.CANNOT_BE_NULL_OR_EMPTY);
                throw new ManahostValidationException(StateObject);
            }
            if (!ValidateModel(search, StateObject))
            {
                throw new ManahostValidationException(StateObject);
            }
            var ObjectSearch = SearchService.GetService(StateObject, CurrentClient, search);
            return new HubModel(200, ObjectSearch);
        }
    }
}