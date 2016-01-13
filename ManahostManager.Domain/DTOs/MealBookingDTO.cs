using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class MealBookingDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        public MealDTO Meal { get; set; }

        // Optional
        // Set only if the manager wants to set a price according to the peoplecategory
        public PeopleCategoryDTO PeopleCategory { get; set; }

        // Mandatory
        public DinnerBookingDTO DinnerBooking { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        // Number of people for this meal
        // Mandatory
        // 1 by default
        [Range(1, int.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int? NumberOfPeople { get; set; }
    }
}