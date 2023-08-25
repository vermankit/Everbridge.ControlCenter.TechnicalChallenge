using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase
{
    public class DoorRepositoryService : IDoorRepositoryService
    {
        private readonly DoorRepositoryDatabaseContext _userRepositoryDatabaseContext;

        public DoorRepositoryService(DoorRepositoryDatabaseContext userRepositoryDatabaseContext)
        {
            _userRepositoryDatabaseContext = userRepositoryDatabaseContext;
        }

        public async Task<List<DoorRecordDto>> GetDoors()
        {
            return await _userRepositoryDatabaseContext.Doors.Select(door => new DoorRecordDto(door)).ToListAsync();
        }

        public async Task<DoorRecordDto> GetDoor(string doorId)
        {
            var door = await _userRepositoryDatabaseContext.Doors.FindAsync(doorId);
            return (door != null) ? new DoorRecordDto(door) : null;
        }

        public async Task<DoorRecordDto> AddDoor(DoorRecordDto door)
        {
            var record = new DoorRecord
            {
                Label = door.Label,
                IsLocked = door.IsLocked,
                IsOpen = door.IsOpen
            };
            await _userRepositoryDatabaseContext.Doors.AddAsync(record);
            await _userRepositoryDatabaseContext.SaveChangesAsync();
            return new DoorRecordDto(record);
        }

        public async Task<DoorRecordDto> RemoveDoor(string doorId)
        {
            var record = await _userRepositoryDatabaseContext.Doors.FindAsync(doorId);
            if (record == null)
            {
                return null;
            }

            _userRepositoryDatabaseContext.Remove(record);
            await _userRepositoryDatabaseContext.SaveChangesAsync();

            return new DoorRecordDto(record);
        }

        public async Task<DoorRecordDto> UpdateDoor(DoorRecordDto door)
        {
            var record = await _userRepositoryDatabaseContext.Doors.FindAsync(door.Id);
            if (record == null)
            {
                return null;
            }

            record.Label = door.Label;
            record.IsLocked = door.IsLocked;
            record.IsOpen = door.IsOpen;
            _userRepositoryDatabaseContext.Attach(record);
            _userRepositoryDatabaseContext.Entry(record).State = EntityState.Modified;
            await _userRepositoryDatabaseContext.SaveChangesAsync();
            return new DoorRecordDto(record);
        }
    }
}
