using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // For a specified period, for a specific room and a specific peoplecategory set a price

    public class PricePerPersonDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Optionnal
        // If this is not set, the Price for the RoomId is a uniq price (not a price per person)
        // it means this is not a price per person but a price for the all room
        public PeopleCategoryDTO PeopleCategory { get; set; }

        // Mandatory
        public PeriodDTO Period { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal PriceHT { get; set; }

        // Mandatory
        public RoomDTO Room { get; set; }

        public TaxDTO Tax { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}