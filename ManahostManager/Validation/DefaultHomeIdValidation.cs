using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;

using System.Web.Http.ModelBinding;

namespace ManahostManager.Validation
{
    public static class DefaultHomeIdValidation
    {
        public static bool DefaultHomeId(ModelStateDictionary validationDictionnary, int? defaultHomeId)
        {
            if (defaultHomeId == null)
                validationDictionnary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Client>(), "DefaultHomeId"), GenericError.DEFAULT_HOME_ID_NOT_SET);
            return validationDictionnary.IsValid;
        }
    }
}