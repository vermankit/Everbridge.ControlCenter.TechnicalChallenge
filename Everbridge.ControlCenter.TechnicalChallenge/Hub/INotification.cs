using System.Threading.Tasks;
using Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase;

namespace Everbridge.ControlCenter.TechnicalChallenge.Hub
{
    public interface INotification
    {
        Task NotifyDoorAdded(DoorRecordDto recordDto);
        Task NotifyDoorUpdated(DoorRecordDto recordDto);
    }
}
