using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // A bill is composed by multiple items

    public class BillItemDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        public BillDTO Bill { get; set; }

        // Description of the bill item
        // Optional
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Description { get; set; }

        // Optional
        // A bill item can be associated with a category
        public BillItemCategoryDTO BillItemCategory { get; set; }

        // A bill item can be grouped with other bill items for discount
        // Optional
        public GroupBillItemDTO GroupBillItem { get; set; }

        // Title of the bill item
        // Mandatory
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public String Title { get; set; }

        // Computed by the API, can be modified by the manager
        public Decimal PriceHT { get; set; }

        // Mandatory
        [Range(0, int.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int Quantity { get; set; }

        // Computed by the API, can be modified by the manager
        public Decimal PriceTTC { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}