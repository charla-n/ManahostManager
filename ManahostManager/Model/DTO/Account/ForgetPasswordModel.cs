using ManahostManager.Utils;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Model.DTO.Account
{
    public class ForgetPasswordModel
    {
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [EmailAddress]
        public string Email { get; set; }
    }
}