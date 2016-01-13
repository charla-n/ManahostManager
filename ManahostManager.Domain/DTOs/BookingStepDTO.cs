using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // Each step of a booking
    // WAITING / VALIDATED / ARCHIVED / ...

    public class BookingStepDTO : IDTO
    {
        public int? Id { get; set; }

        //[RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public BookingStepConfigDTO BookingStepConfig { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        public int? BookingStepIdNext { get; set; }

        //public BookingStepDTO BookingStepNext { get; set; }

        public int? BookingStepIdPrevious { get; set; }

        //public BookingStepDTO BookingStepPrevious { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        public Boolean BookingValidated { get; set; }

        public Boolean BookingArchived { get; set; }

        public DocumentDTO MailTemplate { get; set; }

        public string MailSubject { get; set; }

        public DateTime? DateModification { get; set; }

        public List<DocumentDTO> Documents { get; set; }
    }
}