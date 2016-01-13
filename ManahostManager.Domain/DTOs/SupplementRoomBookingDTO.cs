using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // Add a supplement to a booking room

    public class SupplementRoomBookingDTO : IDTO
    {
        public int? Id { get; set; }

        public RoomBookingDTO RoomBooking { get; set; }

        public RoomSupplementDTO RoomSupplement { get; set; }

        //Computed by the API but can be modified by the user
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceHT { get; set; }

        //Computed by the API but can be modified by the user
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceTTC { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}