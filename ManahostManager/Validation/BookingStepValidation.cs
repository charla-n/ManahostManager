using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System;

namespace ManahostManager.Validation
{
    public class BookingStepValidation : AbstractValidation<BookingStep>
    {
        [Dependency]
        public IBookingStepRepository BookingStepRepository { get; set; }
        protected override bool CommonValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, BookingStep entity, object param, params object[] additionalObjects)
        {
            BookingStep temp = null;

            if (entity.BookingStepNext != null)
                temp = BookingStepRepository.GetBookingStepById(entity.BookingStepNext.Id, currentClient.Id);
            if (temp != null)
            {
                if (entity.BookingStepConfig != null && temp.BookingStepConfig.Id != entity.BookingStepConfig.Id)
                    validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepNext"), GenericError.INVALID_GIVEN_PARAMETER);
                if (entity.BookingArchived && !temp.BookingArchived)
                    validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepNext.BookingArchived"), GenericError.INVALID_GIVEN_PARAMETER);
            }
            if (entity.BookingStepPrevious != null)
                temp = BookingStepRepository.GetBookingStepById(entity.BookingStepPrevious.Id, currentClient.Id);
            if (temp != null)
            {
                if (entity.BookingStepConfig != null && temp.BookingStepConfig.Id != entity.BookingStepConfig.Id)
                    validationDictionary.AddModelError(String.Format(GenericNames.MODEL_STATE_FORMAT, TypeOfName.GetNameFromType<BookingStep>(), "BookingStepPrevious"), GenericError.INVALID_GIVEN_PARAMETER);
            }
            return validationDictionary.IsValid;
        }
    }
}