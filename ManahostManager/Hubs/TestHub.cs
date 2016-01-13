using Microsoft.AspNet.SignalR;

namespace ManahostManager.Hubs
{
    public class TestHub : Hub
    {
        public void Hello()
        {
            Clients.All.sendNotification("COUCOU");
        }
    }
}