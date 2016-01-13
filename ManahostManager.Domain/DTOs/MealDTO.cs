using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class MealDTO : IDTO
    {
        public int? Id { get; set; }

        // Optional
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Description { get; set; }

        // Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String ShortDescription { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Optional category
        public MealCategoryDTO MealCategory { get; set; }

        // Optional
        // True by default
        public Boolean? RefHide { get; set; }

        // Optional
        // Used for associating a meal in a category in the bill, like that when the bill is generated the meal is directly associated with the good category
        public BillItemCategoryDTO BillItemCategory { get; set; }

        public Boolean Hide { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public List<DocumentDTO> Documents { get; set; }
        public List<MealPriceDTO> MealPrices { get; set; }
    }
}