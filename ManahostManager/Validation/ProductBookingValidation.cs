using ManahostManager.Domain.DTOs;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System.Collections.Generic;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Validation
{
    public class ProductBookingValidation : AbstractValidation<ProductBooking>
    {
        protected override bool CommonValidation(ModelStateDictionary validationDictionary, Client currentClient, ProductBooking entity, object param, params object[] additionalObjects)
        {
            if (entity.Booking != null && (entity.Date < entity.Booking.DateArrival || entity.Date > entity.Booking.DateDeparture))
                validationDictionary.AddModelError("Date", GenericError.DOES_NOT_MEET_REQUIREMENTS);
            return validationDictionary.IsValid;
        }

        protected override bool ValidatePut(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, ProductBooking entity, object param, params object[] additionalObjects)
        {
            NullCheckValidation.NullValidation(TypeOfName.GetNameFromType<ProductBooking>(), new Dictionary<string, object>()
                {
                    {"PriceHT", entity.PriceHT},
                    {"PriceTTC", entity.PriceTTC},
                }, validationDictionary);
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }
    }
}