using ExpressiveAnnotations.Attributes;
using ManahostManager.Domain.Entity;
using ManahostManager.Utils;
using System;
using System.ComponentModel.DataAnnotations;

namespace ManahostManager.Domain.DTOs
{
    // Configuration of each question by the manager

    public class SatisfactionConfigQuestionDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public EAnswerType AnswerType { get; set; }

        // Mandatory
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        [MaxLength(2048, ErrorMessage = GenericError.DOES_NOT_MEET_REQUIREMENTS)]
        public String Question { get; set; }

        public SatisfactionConfigDTO SatisfactionConfig { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}