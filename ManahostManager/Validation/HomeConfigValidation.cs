using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Validation
{
    public class HomeConfigValidation : AbstractValidation<HomeConfig>
    {
        [Dependency]
        public IHomeConfigRepository HomeConfigRepository { get; set; }
        protected override bool ValidatePost(ModelStateDictionary validationDictionary, Client currentClient, HomeConfig entity, object param, params object[] additionalObjects)
        {
            if (HomeConfigRepository.GetHomeConfigById(entity.Id, currentClient.Id) != null)
                validationDictionary.AddModelError("HomeConfig", GenericError.ALREADY_EXISTS);
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        protected override bool ValidatePut(ModelStateDictionary validationDictionary, Client currentClient, HomeConfig entity, object param, params object[] additionalObjects)
        {
            if (HomeConfigRepository.GetHomeConfigById(entity.Id, currentClient.Id) == null)
                validationDictionary.AddModelError("HomeConfig", GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        protected override bool ValidateDelete(ModelStateDictionary validationDictionary, Client currentClient, HomeConfig entity, object param, params object[] additionalObjects)
        {
            if (entity.Home.ClientId != currentClient.Id)
                validationDictionary.AddModelError(TypeOfName.GetNameFromType<HomeConfig>(), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            return validationDictionary.IsValid;
        }
    }
}