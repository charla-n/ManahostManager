using ManahostManager.App_Start;
using ManahostManager.Controllers;
using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using ManahostManager.Utils.Mapper;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Services
{
    public interface ISearchService
    {
        dynamic GetService(ModelStateDictionary validationDictionnary, Client client, AdvSearch search);
    }

    public sealed class SearchService : ISearchService
    {
        [Dependency]
        protected ISearchRepository Repo { get; set; }

        [Dependency]
        protected IAdvancedSearch Search { get; set; }

        [Dependency]
        protected IMapper GetMapper { get; set; }

        public dynamic GetService(ModelStateDictionary validationDictionnary, Client currentClient, AdvSearch r)
        {
            if (r == null)
            {
                validationDictionnary.AddModelError(GenericNames.ADVANCED_SEARCH, GenericError.CANNOT_BE_NULL_OR_EMPTY);
                throw new ManahostValidationException(validationDictionnary);
            }
            Search.ToSQL(r.Search, currentClient.Id.ToString(), validationDictionnary);

            if (!validationDictionnary.IsValid)
                throw new ManahostValidationException(validationDictionnary);
            try
            {
                dynamic ret = null;
                Type to = null;

                Search.Infos.includes.UnionWith(r.Include);
                if ((ret = Repo.Search(Search.Infos)) == null)
                    throw new ManahostValidationException(validationDictionnary);
                to = ManahostEntityCaching.Instance.GetTypeFromFullPath(GenericNames.DTO_PATH + Search.Infos.resource + "DTO");
                if (to != null && Search.Infos.count == false)
                {
                    IEnumerable<dynamic> ltmp = ret as IEnumerable<dynamic>;
                    List<dynamic> newList = new List<dynamic>();
                    foreach (var item in ltmp)
                    {
                        newList.Add(GetMapper
    .Map(item, ManahostEntityCaching.Instance.GetTypeFromFullPath(GenericNames.ENTITY_PATH + Search.Infos.resource), to));
                    }
                    return newList;
                }
                return ret;
            }
            catch (Exception e)
            {
                validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, GenericNames.ADVANCED_SEARCH, "search"), GenericError.ADVANCED_SEARCH_SQL_EXCEPTION);
                string stacktrace = e.Message + " " + e.StackTrace + " " + (e.InnerException == null ? "" : (e.InnerException.Message + " " + e.InnerException.StackTrace));
                throw new ManahostValidationException(validationDictionnary, stacktrace);
            }
        }
    }
}