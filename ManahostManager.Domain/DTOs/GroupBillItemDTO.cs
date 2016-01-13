using ExpressiveAnnotations.Attributes;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;

namespace ManahostManager.Domain.DTOs
{
    // A discount can be applied on billitem(s)

    public class GroupBillItemDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public EValueType ValueType { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public Decimal Discount { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}