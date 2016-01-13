using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Validation
{
    public class PricePerPersonValidation : AbstractValidation<PricePerPerson>
    {
        [Dependency]
        public IPricePerPersonRepository PricePerPersonRepository { get; set; }

        override protected bool ValidatePost(ModelStateDictionary validationDictionary, Client currentClient, PricePerPerson entity, object param, params object[] additionalObjects)
        {
            if (PricePerPersonRepository.GetPricePerPersonByRoomIdAndPeriod(entity.Room.Id, entity.Period.Id) != null)
                validationDictionary.AddModelError(TypeOfName.GetNameFromType<PricePerPerson>(), GenericError.ALREADY_EXISTS);
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }
    }
}