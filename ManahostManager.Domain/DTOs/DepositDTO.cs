using ExpressiveAnnotations.Attributes;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class DepositDTO : IDTO, IValidatableObject
    {
        public int? Id { get; set; }

        // Mandatory
        public BookingDTO Booking { get; set; }

        // Set by the API at utcnow
        public DateTime? DateCreation { get; set; }

        // Mandatory
        // Amount or percent
        // Amount as default
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public EValueType ValueType { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Range(1, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal Price { get; set; }

        // Calculated by the API according to the current price of the booking
        // Only calculated if the ValueType is set to PERCENT
        // Can't be modified by the manager
        public Decimal? ComputedPrice { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (ValueType == EValueType.PERCENT && Price > 100)
                yield return new ValidationResult(GenericError.WRONG_DATA, new string[] { "Price" });
        }
    }
}