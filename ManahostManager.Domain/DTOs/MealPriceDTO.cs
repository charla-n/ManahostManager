using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // On each meal a price
    // It is also possible to set the price for a meal for each peoplecategory (children, adults, seniors) but it is optional

    public class MealPriceDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        // TODO Manual check
        public MealDTO Meal { get; set; }

        // Set by the manager
        // Mandatory
        // Set range must be positive
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public Decimal PriceHT { get; set; }

        // Optional Tax
        public TaxDTO Tax { get; set; }

        // Optional
        // Only if the manager wants to set a price different for each people category
        public PeopleCategoryDTO PeopleCategory { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}