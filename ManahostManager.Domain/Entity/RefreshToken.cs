using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.Entity
{
    public class RefreshToken
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string ProtectedTicket { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public DateTime IssuedUtc { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public DateTime ExpiresUtc { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public Service Service { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public Client Client { get; set; }
    }
}