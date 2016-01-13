using ManahostManager.Utils;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Model.DTO.Account
{
    public class ChangePasswordAccountModel
    {
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Compare("Password", ErrorMessage = GenericError.WRONG_DATA)]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}