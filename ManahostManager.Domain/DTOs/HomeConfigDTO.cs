using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class HomeConfigDTO : IDTO, IValidatableObject
    {
        public int? Id { get; set; }

        public Boolean AutoSendSatisfactionEmail { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? DefaultCaution { get; set; }

        public Boolean DepositNotifEnabled { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int? DinnerCapacity { get; set; }

        public Boolean EnableDisplayActivities { get; set; }

        public Boolean EnableDisplayMeals { get; set; }

        public Boolean EnableDisplayProducts { get; set; }

        public Boolean EnableDisplayRooms { get; set; }

        public Boolean EnableReferencing { get; set; }

        public EValueType? DefaultValueType { get; set; }

        public Boolean? EnableDinner { get; set; }

        public Boolean FollowStockEnable { get; set; }

        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Devise { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        // Optional
        // When a booking is cancelled this template is used for sending the email to the customer
        public DocumentDTO BookingCanceledMailTemplate { get; set; }

        // Optional
        // If set, instead of using Manahost account for sending mail, this mail account will be used
        public MailConfigDTO DefaultMailConfig { get; set; }

        // Hours + minutes transformed in minutes
        // if the manager wants the check-in to be at 10:00 this property should be set to (10 * 60)
        public long? DefaultHourCheckIn { get; set; }

        // Hours + minutes transformed in minutes
        // if the manager wants the check-out to be at 10:00 this property should be set to (10 * 60)
        public long? DefaultHourCheckOut { get; set; }

        // True if the manager wants to have 24 hours format
        // False if the manager wants to have 12 hours format
        // True by default
        public Boolean? HourFormat24 { get; set; }

        // Optional
        public BillItemCategory DefaultBillItemCategoryMeal { get; set; }

        // Optional
        public BillItemCategory DefaultBillItemCategoryProduct { get; set; }

        // Optional
        public BillItemCategory DefaultBillItemCategoryRoom { get; set; }

        public int HomeId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        // 23h * 60mins + 59mins
        private static int MAX_HOURS_24H = 1439;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DefaultHourCheckIn != null && (DefaultHourCheckIn < 0 || DefaultHourCheckIn > MAX_HOURS_24H))
                yield return new ValidationResult(GenericError.WRONG_DATA, new string[] { "DefaultHourCheckIn" });
            if (DefaultHourCheckOut != null && (DefaultHourCheckOut < 0 || DefaultHourCheckOut > MAX_HOURS_24H))
                yield return new ValidationResult(GenericError.WRONG_DATA, new string[] { "DefaultHourCheckOut" });
        }
    }
}