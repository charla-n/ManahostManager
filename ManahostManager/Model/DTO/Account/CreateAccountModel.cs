using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Model.DTO.Account
{
    public class CreateAccountModel : IValidatableObject
    {
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Compare("Password", ErrorMessage = GenericError.WRONG_DATA)]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Civility { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public string LastName { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Country { get; set; }

        public double Timezone { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!TimezoneValidation.IsAuthorized(Timezone))
                yield return new ValidationResult(GenericError.WRONG_DATA, new List<string>() { "Timezone" });
        }
    }
}