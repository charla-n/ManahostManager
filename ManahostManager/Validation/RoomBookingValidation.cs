using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;

namespace ManahostManager.Validation
{
    public class RoomBookingValidation : AbstractValidation<RoomBooking>
    {
        [Dependency]
        public IRoomBookingRepository RoomBookingRepository { get; set; }

        [Dependency]
        public IBookingRepository BookingRepository { get; set; }
        [Dependency]
        public IRoomRepository RoomRepository { get; set; }

        private Booking tmp;
        private Room rtmp;

        protected override bool ValidatePut(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, RoomBooking entity, object param, params object[] additionalObjects)
        {
            NullCheckValidation.NullValidation(TypeOfName.GetNameFromType<RoomBooking>(), new Dictionary<String, Object>()
                {
                    {"DateBegin", entity.DateBegin},
                    {"DateEnd", entity.DateEnd}
                }, validationDictionary);
            if (!CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects))
                return false;
            if (tmp != null && rtmp != null &&
                RoomBookingRepository.FindRoomBookingForDates(entity.Room.Id, entity.Id, entity.DateBegin == null ? (DateTime)tmp.DateArrival : (DateTime)entity.DateBegin,
                entity.DateEnd == null ? (DateTime)tmp.DateDeparture : (DateTime)entity.DateEnd, currentClient.Id) != null)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "RoomId"), GenericError.ALREADY_EXISTS);
            return validationDictionary.IsValid;
        }

        protected override bool ValidatePost(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, RoomBooking entity, object param, params object[] additionalObjects)
        {
            if (!CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects))
                return false;
            if (tmp != null && rtmp != null &&
                RoomBookingRepository.FindRoomBookingForDates(entity.Room.Id, 0, entity.DateBegin == null ? (DateTime)tmp.DateArrival : (DateTime)entity.DateBegin,
                entity.DateEnd == null ? (DateTime)tmp.DateDeparture : (DateTime)entity.DateEnd, currentClient.Id) != null)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "RoomId"), GenericError.ALREADY_EXISTS);
            return validationDictionary.IsValid;
        }

        protected override bool CommonValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, RoomBooking entity, object param, params object[] additionalObjects)
        {
            if (entity.Booking != null)
                tmp = BookingRepository.GetBookingById((int)entity.Booking.Id, currentClient.Id);
            if (entity.Room != null)
                rtmp = RoomRepository.GetRoomById((int)entity.Room.Id, currentClient.Id);
            if (entity.DateBegin != null && tmp != null && (entity.DateBegin < tmp.DateArrival || entity.DateBegin > tmp.DateDeparture || (entity.DateEnd != null && entity.DateBegin >= entity.DateEnd)))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateBegin"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            if (entity.DateEnd != null && tmp != null && (entity.DateEnd < tmp.DateArrival || entity.DateEnd > tmp.DateDeparture || (entity.DateBegin != null && entity.DateEnd <= entity.DateBegin)))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<RoomBooking>(), "DateEnd"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            return validationDictionary.IsValid;
        }
    }
}