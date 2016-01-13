using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // The bill can be split in multiple payment method
    // For example you can pay 50% of the price by cash and the other 50% by credit card

    public class PaymentMethodDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        public BillDTO Bill { get; set; }

        // Mandatory
        // The payment type (cash, credit card, ...)
        public PaymentTypeDTO PaymentType { get; set; }

        // Mandatory
        [Range(1, double.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public Decimal Price { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}