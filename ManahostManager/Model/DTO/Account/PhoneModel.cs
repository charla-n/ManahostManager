using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Model.DTO.Account
{
    public class PhoneModel
    {
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public bool IsPrimary
        {
            get;
            set;
        }

        [Phone]
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string Phone
        {
            get;
            set;
        }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public PhoneType Type
        {
            get;
            set;
        }
    }
}