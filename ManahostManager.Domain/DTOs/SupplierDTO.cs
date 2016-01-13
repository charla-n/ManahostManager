using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ManahostManager.Domain.DTOs
{
    public class SupplierDTO : IDTO, IValidatableObject
    {
        public int? Id { get; set; }

        // Mandatory
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public String SocietyName { get; set; }

        // Mandatory / Optional
        [MaxLength(1024, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Addr { get; set; }

        // Mandatory / Optional
        [MaxLength(1024, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Contact { get; set; }

        // Mandatory / Optional
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Comment { get; set; }

        // Set by the API, can't be modified by the manager
        public DateTime? DateCreation { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Email { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Phone1 { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Phone2 { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        public Boolean Hide { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // If email is an email
            if (Email != null && !MailValid.IsValid(Email))
                yield return new ValidationResult(GenericError.WRONG_DATA, new[] { "Email" });
            // Phone must contain only digits
            if (Phone1 != null && !Regex.IsMatch(Phone1, GenericNames.ONLY_NUMBER_REGEX))
                yield return new ValidationResult(GenericError.WRONG_DATA, new[] { "Phone1" });
            // Phone must contain only digits
            if (Phone2 != null && !Regex.IsMatch(Phone2, GenericNames.ONLY_NUMBER_REGEX))
                yield return new ValidationResult(GenericError.WRONG_DATA, new[] { "Phone2" });
        }
    }
}