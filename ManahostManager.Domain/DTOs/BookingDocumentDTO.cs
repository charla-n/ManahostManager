using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;

namespace ManahostManager.Domain.DTOs
{
    // A booking is linked to multiple documents
    // Contrat de location, plan d'arrivée à envoyer via un mail, état des lieux, ...

    public class BookingDocumentDTO : IDTO
    {
        public int? Id { get; set; }

        public BookingDTO Booking { get; set; }

        public DocumentDTO Document { get; set; }

        // Needs to be sent for each request
        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }
    }
}