using ManahostManager.Domain.DAL;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;
using System.Linq;

namespace ManahostManager.Validation
{
    public interface IValidation
    {
    }

    public interface IValidation<T> : IValidation
    {
        bool PreValidatePost(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects);

        bool PreValidatePut(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects);

        bool PreValidateDelete(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects);
    }

    public class AbstractValidation<T> : IValidation<T>

    {

        [Dependency]
        public ManahostManagerDAL Context { get; set; }

        public bool PreValidatePost(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects)
        {
            if (!PreCommonValidation(validationDictionary, currentClient, entity, param, additionalObjects))
                return false;
            return ValidatePost(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        public bool PreValidatePut(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects)
        {
            if (!PreCommonValidation(validationDictionary, currentClient, entity, param, additionalObjects))
                return false;
            return ValidatePut(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        protected virtual bool ValidatePost(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects)
        {
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        protected virtual bool ValidatePut(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects)
        {
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        protected virtual bool CommonValidation(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects)
        {
            return validationDictionary.IsValid;
        }

        protected bool PreCommonValidation(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects)
        {
            IEntity cEntity = entity as IEntity;

            if (cEntity != null && cEntity.GetFK() != null)
            {
                int homeid = (int)cEntity.GetFK();
                //repo.IgnoreIncludes = true;
                //if (repo.GetUniq<Home>(p => p.Id == homeid && p.ClientId == currentClient.Id) == null)
                //   validationDictionary.AddModelError("HomeId", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                if (Context.Set<Home>().FirstOrDefault(p => p.Id == homeid && p.ClientId == currentClient.Id) == null)
                {
                    validationDictionary.AddModelError("HomeId", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
                }
            }
            return validationDictionary.IsValid;
        }

        protected virtual bool ValidateDelete(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects)
        {
            return validationDictionary.IsValid;
        }

        public bool PreValidateDelete(ModelStateDictionary validationDictionary, Client currentClient, T entity, object param, params object[] additionalObjects)
        {
            return ValidateDelete(validationDictionary, currentClient, entity, param, additionalObjects);
        }
    }
}