using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class AdditionalBookingDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public decimal PriceHT { get; set; }

        // Computed by the API but can be modified by the owner manually
        // For computing it you need to call /Compute/id
        public decimal? PriceTTC { get; set; }

        // Mandatory
        // TODO Manual Check
        //[RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public BookingDTO Booking { get; set; }

        public BillItemCategoryDTO BillItemCategory { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public TaxDTO Tax { get; set; }
    }
}