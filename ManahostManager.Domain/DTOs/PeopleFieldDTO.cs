using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // See explanation in FieldGroup Entity

    public class PeopleFieldDTO : IDTO, IValidatableObject
    {
        public int? Id { get; set; }

        public FieldGroupDTO FieldGroup { get; set; }

        public PeopleDTO People { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Key { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Value { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (People == null && FieldGroup == null)
                yield return new ValidationResult(GenericError.CANNOT_BE_NULL_OR_EMPTY, new string[] { "PeopleId&FieldGroupId" });
            if (People != null && FieldGroup != null)
                yield return new ValidationResult(GenericError.DOES_NOT_MEET_REQUIREMENTS, new string[] { "PeopleId&FieldGroupId" });
        }
    }
}