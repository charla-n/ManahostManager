using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManahostManager.Domain.DTOs
{
    // The manager is able to send mail with Manahost

    public class MailConfigDTO : IDTO, IValidatableObject
    {
        public int? Id { get; set; }

        // Regex @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}\b"
        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(1024, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Email { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(2048, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Smtp { get; set; }

        // Mandatory
        // Set range from 0 to 65535
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int SmtpPort { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public bool? IsSSL { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        [NotMapped]
        private const int LIMIT_PORT = 65535;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // If it's not an email
            if (Email != null && !MailValid.IsValid(Email))
                yield return new ValidationResult(GenericError.DOES_NOT_MEET_REQUIREMENTS, new string[] { "Email" });
            // smtpPort must be between 0 and 65535
            if (SmtpPort < 0 || SmtpPort > LIMIT_PORT)
                yield return new ValidationResult(GenericError.DOES_NOT_MEET_REQUIREMENTS, new string[] { "SmtpPort" });
        }
    }
}