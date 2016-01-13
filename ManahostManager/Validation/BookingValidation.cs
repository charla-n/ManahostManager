using ManahostManager.Controllers;
using ManahostManager.Domain.Entity;
using ManahostManager.Domain.Repository;
using ManahostManager.Utils;
using Microsoft.Practices.Unity;
using System;
using System.Linq;

namespace ManahostManager.Validation
{
    public class BookingValidation : AbstractValidation<Booking>
    {
        private const int MAX_NIGHTS = 365;

        [Dependency]
        public IPeriodRepository PeriodRepository { get; set; }

        protected override bool CommonValidation(System.Web.Http.ModelBinding.ModelStateDictionary validationDictionary, Client currentClient, Booking entity, object param, params object[] additionalObjects)
        {
            double elapsed;

            // If DateDeparture - DateArrive < 1 or if the booking time is more than 365 days
            if (entity.DateArrival != null && entity.DateDeparture != null && ((elapsed = DateUtils.GetElapsedDaysFromDateTimes(entity.DateArrival, entity.DateDeparture)) < 1 || elapsed > MAX_NIGHTS))
                validationDictionary.AddModelError("Dates", GenericError.WRONG_DATA);
            if (entity.DateArrival != null && entity.DateDeparture != null &&
                !PeriodUtils.AllCovered(PeriodRepository.GetPeriodByDates((DateTime)entity.DateArrival, (DateTime)entity.DateDeparture, currentClient.Id).ToList(), (DateTime)entity.DateArrival, (DateTime)entity.DateDeparture))
                validationDictionary.AddModelError("Period", GenericError.DOES_NOT_MEET_REQUIREMENTS);
            return validationDictionary.IsValid;
        }
    }
}