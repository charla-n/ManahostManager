using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // Configuration of the satisfaction survey by the manager

    public class SatisfactionConfigDTO : IDTO
    {
        public int? Id { get; set; }

        // AdditionalInformations at the bottom (rules, ...)
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String AdditionalInfo { get; set; }

        // Add a description ?
        [MaxLength(4000, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Description { get; set; }

        // Title of satisfaction
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(256, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Title { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public List<SatisfactionConfigQuestionDTO> SatisfactionConfigQuestions { get; set; }

        public int HomeId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }
}