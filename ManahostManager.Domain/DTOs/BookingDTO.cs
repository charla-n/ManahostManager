using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class BookingDTO : IDTO
    {
        public int? Id { get; set; }

        // A comment made by the manager
        // Optional
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Comment { get; set; }

        // Set by the manager
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public DateTime DateArrival { get; set; }

        // Set by the API at utcNow
        public DateTime? DateCreation { get; set; }

        // Set by the manager
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public DateTime DateDeparture { get; set; }

        // Set by the manager
        public DateTime? DateDesiredPayment { get; set; }

        // Set by the API at utcNow
        public DateTime? DateModification { get; set; }

        // Set by bookingstep when the step has the boolean bookingvalidated at true but can be modified by the manager in case of no bookingstepconfig are set
        public DateTime? DateValidation { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Later ...
        // False by default
        // Do not show it for now
        public Boolean IsOnline { get; set; }

        // Optional
        // False by default
        public Boolean IsSatisfactionSended { get; set; }

        // A booking is associated with a person
        // Optional
        public PeopleDTO People { get; set; }

        // Set by the API according to PeopleBooking
        // Total People coming for a booking
        // Set at 0 when a booking is created
        public int TotalPeople { get; set; }

        public List<AdditionalBookingDTO> AdditionalBookings { get; set; }

        public List<BookingDocumentDTO> BookingDocuments { get; set; }

        public BookingStepBookingDTO BookingStepBooking { get; set; }

        public List<DepositDTO> Deposits { get; set; }

        public List<DinnerBookingDTO> DinnerBookings { get; set; }

        public List<ProductBookingDTO> ProductBookings { get; set; }

        public List<RoomBookingDTO> RoomBookings { get; set; }
    }
}