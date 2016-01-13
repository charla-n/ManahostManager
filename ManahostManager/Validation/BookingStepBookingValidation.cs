using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Model;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Text;
using System.Web.Http.ModelBinding;

namespace ManahostManager.Validation
{
    public class BookingStepBookingValidation : AbstractValidation<BookingStepBooking>
    {
        private const int MAX_SENT = 2;

        [Dependency]
        public IBookingStepBookingRepository BookingStepBookingRepository { get; set; }

        protected override bool ValidatePost(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, BookingStepBooking entity, object param, params object[] additionalObjects)
        {
            if (entity.BookingStepConfig == null)
                validationDictionary.AddModelError("CurrentStep", GenericError.CANNOT_BE_NULL_OR_EMPTY);
            if (BookingStepBookingRepository.GetBookingStepBookingById(entity.Id, currentClient.Id) != null)
                validationDictionary.AddModelError("BookingStepBooking", GenericError.ALREADY_EXISTS);
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        protected override bool ValidatePut(ModelStateDictionary validationDictionary, Client currentClient, BookingStepBooking entity, object param, params object[] additionalObjects)
        {
            if (entity.BookingStepConfig == null)
                validationDictionary.AddModelError("CurrentStep", GenericError.CANNOT_BE_NULL_OR_EMPTY);
            return CommonValidation(validationDictionary, currentClient, entity, param, additionalObjects);
        }

        public virtual bool MailStepValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, HomeConfig hc, BookingStepBooking entity, MailBookingModel model, object param, params object[] additionalObjects)
        {
            if (hc == null)
            {
                validationDictionary.AddModelError("HomeConfig", GenericError.CANNOT_BE_NULL_OR_EMPTY);
                return false;
            }
            if (hc.DefaultMailConfig != null && String.IsNullOrEmpty(model.Password))
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MailBookingModel>(), "Password"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
            try
            {
                if (hc.DefaultMailConfig != null && !String.IsNullOrEmpty(model.Password))
                    model.Password = Encoding.UTF8.GetString(Convert.FromBase64String(model.Password));
            }
            catch (Exception e)
            {
                if (e is FormatException || e is ArgumentNullException)
                    validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<MailBookingModel>(), "Password"), GenericError.INVALID_GIVEN_PARAMETER);
                else
                    throw;
            }
            if (hc.DefaultMailConfig == null && entity.MailSent == MAX_SENT)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Booking>(), "MailSent"), GenericError.DOES_NOT_MEET_REQUIREMENTS);
            if (entity.CurrentStep == null)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Booking>(), "CurrentStep"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
            if (entity.CurrentStep != null && entity.CurrentStep.MailTemplate == null)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Booking>(), "MailTemplate"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
            if (entity.Booking.People == null)
                validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<Booking>(), "People"), GenericError.CANNOT_BE_NULL_OR_EMPTY);
            return validationDictionary.IsValid;
        }
    }
}