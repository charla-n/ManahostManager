using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class ProductDTO : IDTO
    {
        public int? Id { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public String Title { get; set; }

        // Optional
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String ShortDescription { get; set; }

        // Mandatory
        // Set a range, must be positive
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Range(0, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Decimal? PriceHT { get; set; }

        public ProductCategoryDTO ProductCategory { get; set; }

        // Optional
        // True by default
        public Boolean? RefHide { get; set; }

        // Optional
        // Must be positive
        [Range(0, int.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int? Stock { get; set; }

        public SupplierDTO Supplier { get; set; }

        public TaxDTO Tax { get; set; }

        // For being alerted if the stock reach the threshold
        [Range(0, int.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int? Threshold { get; set; }

        // Must be positive
        // In seconds
        [Range(0, Int64.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public Int64? Duration { get; set; }

        public BillItemCategoryDTO BillItemCategory { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public Boolean Hide { get; set; }

        public bool IsService { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        // Managed by the API
        public bool? IsUnderThreshold { get; set; }

        public List<DocumentDTO> Documents { get; set; }
    }
}