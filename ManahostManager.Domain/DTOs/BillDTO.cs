﻿using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class BillDTO : IDTO
    {
        public int? Id { get; set; }

        // Set by the API at utcnow
        public DateTime CreationDate { get; set; }

        // Optional
        // The result of a generated bill
        public DocumentDTO Document { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // False by default
        // Set by the manager
        public Boolean IsPayed { get; set; }

        // ex: R2014-0001
        // Generated by the API
        [MaxLength(64, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Reference { get; set; }

        // Optional
        // A bill issued by a supplier for the establishment
        public SupplierDTO Supplier { get; set; }

        // Computed by the API, can be modified by the manager
        public Decimal TotalTTC { get; set; }

        // Computed by the API, can be modified by the manager
        public Decimal TotalHT { get; set; }

        // Optional
        // A bill is not necessarily associated with a booking
        // A manager is able to create its own bill for what he wants
        public BookingDTO Booking { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public List<BillItemDTO> BillItems { get; set; }
        public List<PaymentMethodDTO> PaymentMethods { get; set; }
    }
}