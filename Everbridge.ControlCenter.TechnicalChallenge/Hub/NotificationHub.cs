using Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase;
using Microsoft.AspNetCore.SignalR;

namespace Everbridge.ControlCenter.TechnicalChallenge.Hub
{
    public class NotificationHub : Hub<INotification>
    {
        public void NotifyDoorAdded(DoorRecordDto recordDto)
        {
            Clients.All.NotifyDoorAdded(recordDto);
        }

        public void NotifyDoorUpdated(DoorRecordDto recordDto)
        {
            Clients.All.NotifyDoorUpdated(recordDto);
        }
    }
}
