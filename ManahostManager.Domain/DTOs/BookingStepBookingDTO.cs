using ExpressiveAnnotations.Attributes;
using ManahostManager.Utils;
using System;

namespace ManahostManager.Domain.DTOs
{
    // Relationship one to one
    // A booking needs to have steps for going through each steps of the booking from the creation to the validation

    public class BookingStepBookingDTO : IDTO
    {
        public int? Id { get; set; }

        // Mandatory
        public BookingStepConfigDTO BookingStepConfig { get; set; }

        // Mandatory
        // On the creation the API will automatically set this property at the very first step
        public BookingStepDTO CurrentStep { get; set; }

        // If CurrentStep.SendMail is set at true this integer tells if the mail has been sent or no
        // Set at 0 by default
        // 0 = never sent
        // 1 = sent 1 time
        // 2 = sent 2 times
        // Can't be more than 2
        // Can't be modified by the manager
        public int MailSent { get; set; }

        // If CurrentStep.MailTemplateId is set this foreignkey tells you if the mail has been successfully sent
        // Set by the API can't be modified by the manager
        public MailLogDTO MailLog { get; set; }

        [RequiredIf("Id == 0", ErrorMessage = GenericError.CANNOT_BE_NULL_OR_EMPTY)]
        public int HomeId { get; set; }

        public BookingDTO Booking { get; set; }

        // Set by the API can't be modified by the manager
        // Used to known when the currentStep has changed
        public DateTime? DateCurrentStepChanged { get; set; }

        // Set by the API can't be modified by the manager
        public DateTime? DateModification { get; set; }

        // If true the booking is canceled
        // False by default
        public bool Canceled { get; set; }
    }
}