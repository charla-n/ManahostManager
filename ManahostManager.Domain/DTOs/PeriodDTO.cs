using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // Definition of a period (haute saison, basse saison, week-end, vacances, fermetures, ...)

    public class PeriodDTO : IDTO
    {
        public int? Id { get; set; }

        // Beginning of the period
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public DateTime Begin { get; set; }

        // End of the period
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public DateTime End { get; set; }

        // Set this period only for some days
        // Monday : 1
        // Tuesday : 2
        // Wednesday : 4
        // Thursday : 8
        // Friday : 16
        // Saturday : 32
        // Sunday : 64
        // Combine them with | operator
        // A lot of validation must be done here on client side
        // The period from 01-01 to 12-31 with Monday to Friday gives M | T | W | T | F as days value
        // The manager also need to specify a new period for week-ends from 01-01 12-31 because the full year is not cover
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [Range(1, int.MaxValue, ErrorMessage = GenericError.WRONG_DATA)]
        public int Days { get; set; }

        // Optional
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Description { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Mandatory
        // By default false
        public Boolean IsClosed { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}