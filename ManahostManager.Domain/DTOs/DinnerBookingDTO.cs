using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class DinnerBookingDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        public BookingDTO Booking { get; set; }

        // Set by the manager
        // Date of the dinner
        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public DateTime? Date { get; set; }

        // Optional
        public int? NumberOfPeople { get; set; }

        // Computed by the API but the manager can modify
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceHT { get; set; }

        // Computed by the API but the manager can modify
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceTTC { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public List<MealBookingDTO> MealBookings { get; set; }
    }
}