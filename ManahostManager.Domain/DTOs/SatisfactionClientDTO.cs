using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;
using System.Collections.Generic;

namespace ManahostManager.Domain.DTOs
{
    // Satisfaction of a client is a one shot, he can't modify its responses after, neither the manager (he's just able to delete)

    public class SatisfactionClientDTO : IDTO
    {
        public int? Id { get; set; }

        // In case the client has an account on Manahost (later for the referencement)
        public ClientDTO ClientDest { get; set; }

        // Set by the API at utcnow
        public DateTime? DateAnswered { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // In case the client does not have an account on Manahost
        public PeopleDTO PeopleDest { get; set; }

        public BookingDTO Booking { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        public List<SatisfactionClientAnswerDTO> SatisfactionClientAnswers { get; set; }
    }
}