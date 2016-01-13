using ManahostManager.Utils;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.Entity
{
    public enum ApplicationTypes
    {
        JAVASCRIPT = 0,
        NATIVE_CLIENT = 1
    };

    public class Service
    {
        [Key]
        public string Id { get; set; }

        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string Secret { get; set; }

        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        [Required(ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public string Name { get; set; }

        public bool Active { get; set; }

        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public string AllowedOrigin { get; set; }

        public ApplicationTypes ApplicationType { get; set; }

        public int RefreshTokenLifeTime { get; set; }
    }
}