using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class BedDTO : IDTO
    {
        public int? Id { get; set; }

        // Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Description { get; set; }

        // Optional
        [Range(0, Int32.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int? NumberPeople { get; set; }

        public RoomDTO Room { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}