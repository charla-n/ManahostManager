using ManahostManager.Model;

namespace ManahostManager.InterfaceHubs
{
    public interface INotification
    {
        void postNotification(NotificationModel model);
    }
}