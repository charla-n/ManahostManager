using ManahostManager.Utils;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.Entity
{
    public enum PhoneType
    {
        HOME,
        WORK,
        MOBILE
    }

    public class PhoneNumber
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public PhoneType PhoneType { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string Phone { get; set; }
    }
}