using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // A BookingStepConfig has multiple step
    // A manager is able to create multiple step config

    public class BookingStepConfigDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public List<BookingStepDTO> BookingSteps { get; set; }
    }
}