using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class PeopleDTO : IDTO, IValidatableObject
    {
        public int? Id { get; set; }

        // Mandatory
        // By default false
        public Boolean AcceptMailing { get; set; }

        // Mandatory / Optional
        [MaxLength(1024, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Addr { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String City { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Civility { get; set; }

        // Mandatory / Optional
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Comment { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Country { get; set; }

        // Mandatory / Optional
        public DateTime? DateBirth { get; set; }

        // Set by the API, can't be modified by the manager
        public DateTime? DateCreation { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Email { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public String Firstname { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public String Lastname { get; set; }

        //Min 0 Max 5
        // Optional
        [Range(0, 5, ErrorMessage = GenericError.WRONG_DATA)]
        public Nullable<int> Mark { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Phone1 { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Phone2 { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String State { get; set; }

        // Mandatory / Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String ZipCode { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // By default false
        public Boolean Hide { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // If datebirth > UtcNow
            if (DateBirth > DateTime.UtcNow)
                yield return new ValidationResult(GenericError.WRONG_DATA, new[] { "DateBirth" });
            // If email is not an email
            if (Email != null && !MailValid.IsValid(Email))
                yield return new ValidationResult(GenericError.WRONG_DATA, new[] { "Email" });
            // If acceptMailing is at true you must provide an email
            if (Email == null && AcceptMailing == true)
                yield return new ValidationResult(GenericError.CANNOT_BE_NULL_OR_EMPTY, new[] { "Email" });
        }
    }
}