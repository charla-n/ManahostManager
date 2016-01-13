using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace ManahostManager.Validation
{
    public class PeriodValidation : AbstractValidation<Period>
    {
        [Dependency]
        public IPeriodRepository PeriodRepository { get; set; }

        protected override bool ValidatePost(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, Period entity, object param, params object[] additionalObjects)
        {
            if (param != null && entity != null && entity.End != null && entity.Begin != null && entity.Days > 0)
            {
                List<Period> periodList = PeriodRepository.GetPeriodByDates(entity.Begin, entity.End, currentClient.Id).ToList();
                if (PeriodUtils.IsDaysCross(periodList, entity))
                    validationDictionary.AddModelError(TypeOfName.GetNameFromType<Period>(), GenericError.ALREADY_EXISTS);
            }
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        protected override bool ValidatePut(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, Period entity, object param, params object[] additionalObjects)
        {
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }
    }
}