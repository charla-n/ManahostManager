using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class RoomBookingDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        //[RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public BookingDTO Booking { get; set; }

        // Computed by the API but can be modified by the manager
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceHT { get; set; }

        // Computed by the API but can be modified by the manager
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceTTC { get; set; }

        public RoomDTO Room { get; set; }

        // If the manager want to set a different date for the roombooking
        // For example if someone do a reservation of 2 rooms but wants to delay the booking of one room because some other people come later
        // Set by default at DateArrival from Booking
        public DateTime? DateBegin { get; set; }

        // If the manager want to set a different date for the roombooking
        // For example if someone do a reservation of 2 rooms but wants to delay the booking of one room because some other people come later
        // Set by default at DateDeparture from Booking
        public DateTime? DateEnd { get; set; }

        // Computed by the API, can't be modified by the manager
        public int? NbNights { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public List<PeopleBookingDTO> PeopleBookings { get; set; }
        public List<SupplementRoomBookingDTO> SupplementRoomBookings { get; set; }
    }
}