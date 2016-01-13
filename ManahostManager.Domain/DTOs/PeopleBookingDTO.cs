using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // How many and which type of people are in a room

    public class PeopleBookingDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        public RoomBookingDTO RoomBooking { get; set; }

        // Mandatory
        public PeopleCategoryDTO PeopleCategory { get; set; }

        // Mandatory
        [Range(1, int.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int NumberOfPeople { get; set; }

        // The manager is able to specify when a people is coming for a booking
        // By default this is set at RoomBooking.DateBegin
        public DateTime? DateBegin { get; set; }

        // The manager is able to specify when a people is leaving for a booking
        // By default this is set at RoomBooking.DateEnd
        public DateTime? DateEnd { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}