namespace ManahostManager.Model
{
    public class NotificationModel
    {
        public enum TypeNotification
        {
            CREATE = 1,
            UPDATE,
            DELETE
        }

        public enum CategoryNotification
        {
            ROOM = 1,
            RESERVATION
        }

        public CategoryNotification Category { get; set; }

        public TypeNotification Type { get; set; }
    }
}