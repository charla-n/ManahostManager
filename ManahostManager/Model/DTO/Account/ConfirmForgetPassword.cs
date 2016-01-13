using ManahostManager.Utils;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Model.DTO.Account
{
    public class ConfirmForgetPassword
    {
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string Token { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Compare("Password", ErrorMessage = GenericError.WRONG_DATA)]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}