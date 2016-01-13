using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class ProductBookingDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        public BookingDTO Booking { get; set; }

        // In case of a performance (prestation)
        public DateTime? Date { get; set; }

        // In case of a performance (prestation)
        // Computed by the API according to the Duration in Product.Duration and Quantity
        // For computing it you need to call /Compute/id
        // Can be modified by the manager
        [Range(0, int.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Int64? Duration { get; set; }

        // Computed by the API but can be modified by the manager according to Quantity
        // For computing it you need to call /Compute/id
        // Can be modified by the manager
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceHT { get; set; }

        // Computed by the API but can be modified by the manager according to Product.Tax and Quantity
        // For computing it you need to call /Compute/id
        // Can be modified by the manager
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceTTC { get; set; }

        public ProductDTO Product { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Range(0, int.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int Quantity { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}