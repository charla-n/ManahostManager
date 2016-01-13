using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;

using System.Web.Http.ModelBinding;

namespace ManahostManager.Validation
{
    public static class TimeZoneValidation
    {
        public static bool Validate(ModelStateDictionary validationDictionary, String TZ)
        {
            if (TZ != null)
            {
                foreach (TimeZoneInfo z in SystemTimeZoneSingleton.Instance.TimeZones)
                {
                    if (z.Id.Equals(TZ))
                    {
                        return validationDictionary.IsValid;
                    }
                }
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Client>(), "Timezone"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            }
            return validationDictionary.IsValid;
        }
    }
}