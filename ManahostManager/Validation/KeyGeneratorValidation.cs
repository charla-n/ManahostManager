using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;
using System.Linq;

namespace ManahostManager.Validation
{
    public class KeyGeneratorValidation : AbstractValidation<KeyGenerator>
    {
        private readonly EKeyType[] managerKeyTypes = { EKeyType.MANAHOST, EKeyType.BETA };
        private readonly EKeyType[] priceShoudntBeNullForKeyTypes = { EKeyType.CLIENT, EKeyType.MANAHOST };

        protected override bool CommonValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, KeyGenerator entity, object param, params object[] additionalObjects)
        {
            if (!currentClient.IsManager && managerKeyTypes.Any(p => p == entity.KeyType))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "KeyType"), GenericError.FORBIDDEN_RESOURCE_OR_DOES_NO_EXIST);
            if (entity.DateExp != null && DateTime.Compare((DateTime)entity.DateExp, DateTime.Now) < 0)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "DateExp"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            if (entity.Price != null && entity.Price < 0)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "Price"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            if ((entity.ValueType < 0 || entity.ValueType >= EValueType.VALUE_MAX_ENUM))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "ValueType"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            if ((entity.KeyType < 0 || entity.KeyType >= EKeyType.VALUE_MAX_ENUM))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<KeyGenerator>(), "KeyType"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            return validationDictionary.IsValid;
        }
    }
}