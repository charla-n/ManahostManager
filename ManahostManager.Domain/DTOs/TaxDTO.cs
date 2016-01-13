using ExpressiveAnnotations.Attributes;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class TaxDTO : IDTO, IValidatableObject
    {
        public int? Id { get; set; }

        //Percent or Amount
        //Optional set at AMOUNT by default
        public EValueType ValueType { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Mandatory
        // Has to be strictly superior of 0
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal Price { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // If the valueType is Percent and the Price > 100 an error is raised
            if (ValueType == EValueType.PERCENT && Price > 100)
                yield return new ValidationResult(GenericError.WRONG_DATA, new[] { "Price" });
        }
    }
}