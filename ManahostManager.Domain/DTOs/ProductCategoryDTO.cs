using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class ProductCategoryDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public String Title { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Optional
        // Set at true by default
        public Boolean? RefHide { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}