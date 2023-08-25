using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase
{
    public interface IDoorRepositoryService
    {
        Task<List<DoorRecordDto>> GetDoors();
        Task<DoorRecordDto> GetDoor(string doorId);
        Task<DoorRecordDto> AddDoor(DoorRecordDto door);
        Task<DoorRecordDto> RemoveDoor(string doorId);
        Task<DoorRecordDto> UpdateDoor(DoorRecordDto door);
    }
}
