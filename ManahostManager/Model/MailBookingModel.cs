namespace ManahostManager.Model
{
    public class MailBookingModel
    {
        // The booking id
        // The API will use this id and get the CurrentStep in BookingStepBooking and then send the good MailTemplate which is associated with the CurrentStep (CurrentStep is a BookingStep)
        public int BookingId { get; set; }

        // The password of the mail account if DefaultMailConfigId is set
        // If HomeConfig.DefaultMailConfigId is set the Manahost mail account will not be used anymore
        // instead the mail account specified by the manager will be used
        // Base 64
        public string Password { get; set; }
    }
}