using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace ManahostManager.Validation
{
    public class PeopleBookingValidation : AbstractValidation<PeopleBooking>
    {
        [Dependency]
        public IRoomBookingRepository RoomBookingRepository { get; set; }
        protected override bool ValidatePut(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, PeopleBooking entity, object param, params object[] additionalObjects)
        {
            NullCheckValidation.NullValidation(TypeOfName.GetNameFromType<PeopleBooking>(), new Dictionary<string, object>()
                {
                    {"DateBegin", entity.DateBegin},
                    {"DateEnd", entity.DateEnd},
                }, validationDictionary);
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        protected override bool CommonValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, PeopleBooking entity, object param, params object[] additionalObjects)
        {
            RoomBooking rbtmp = null;

            if (entity.RoomBooking != null)
                rbtmp = RoomBookingRepository.GetRoomBookingById(entity.RoomBooking.Id, currentClient.Id);
            if (entity.DateBegin != null && entity.DateEnd != null && (entity.DateBegin >= entity.DateEnd || DateUtils.GetElapsedDaysFromDateTimes((DateTime)entity.DateBegin, (DateTime)entity.DateEnd) < 1))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "Dates"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            else if (entity.DateEnd != null && rbtmp != null && entity.DateEnd > rbtmp.DateEnd)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<PeopleBooking>(), "DateEnd"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            return validationDictionary.IsValid;
        }
    }
}