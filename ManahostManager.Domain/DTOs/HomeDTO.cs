using ExpressiveAnnotations.Attributes;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    public class HomeDTO : IDTO
    {
        public int? Id { get; set; }

        // Set by the manager
        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public EEstablishmentType EstablishmentType { get; set; }

        // Set by the manager
        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Optionnal
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Address { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public int HomeId
        {
            get
            {
                return 0;
            }

            set
            {
            }
        }
    }
}