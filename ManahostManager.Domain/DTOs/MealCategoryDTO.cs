using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // Each meal associated with an optional category (drink, dessert, meat, fish, ...)

    public class MealCategoryDTO : IDTO
    {
        public int? Id { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Label { get; set; }

        // Optional
        // True by default
        public Boolean? RefHide { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}