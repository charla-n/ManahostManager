using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class RoomDTO : IDTO
    {
        public int? Id { get; set; }

        // Optional
        // Set a caution for a room
        [Range(0, Double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? Caution { get; set; }

        // Official / Unofficial
        // Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public string Classification { get; set; }

        // Optional
        // Format : 0xFF00FF
        // Default 0x000000
        [Range(0x000000, 0xFFFFFF, ErrorMessage = GenericError.WRONG_DATA)]
        public int? Color { get; set; }

        // Optional
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Description { get; set; }

        // Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String ShortDescription { get; set; }

        // Set at false by default
        // Default false
        public Boolean IsClosed { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Optional
        // Default true
        public Boolean? RefHide { get; set; }

        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String RoomState { get; set; }

        // Optional
        // Square meter
        [Range(0, Int32.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int? Size { get; set; }

        // Default false
        public Boolean Hide { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Range(0, Int32.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int? Capacity { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        public RoomCategoryDTO RoomCategory { get; set; }

        public BillItemCategoryDTO BillItemCategory { get; set; }

        public List<BedDTO> Beds { get; set; }

        public List<DocumentDTO> Documents { get; set; }
    }
}