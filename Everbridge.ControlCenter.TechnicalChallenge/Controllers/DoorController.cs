using Everbridge.ControlCenter.TechnicalChallenge.DoorDatabase;
using Everbridge.ControlCenter.TechnicalChallenge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Everbridge.ControlCenter.TechnicalChallenge.Hub;
using Microsoft.AspNetCore.SignalR;

namespace Everbridge.ControlCenter.TechnicalChallenge.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class DoorController : ControllerBase
    {
        private readonly ILogger<DoorController> _logger;
        private readonly IDoorRepositoryService _doorRepositoryService;
        private readonly IHubContext<NotificationHub,INotification> _notificationHubContext ;
        public DoorController(ILogger<DoorController> logger, IDoorRepositoryService doorRepositoryService, IHubContext<NotificationHub,INotification> notificationHubContext)
        {
            _logger = logger;
            _doorRepositoryService = doorRepositoryService;
            _notificationHubContext = notificationHubContext;
        }

        [HttpGet]
        public async Task<IEnumerable<DoorModel>> Get()
        {
            var doorsRecords = await _doorRepositoryService.GetDoors();

            return doorsRecords.Select(doorRecord => new DoorModel()
            {
                Id = doorRecord.Id,
                Label = doorRecord.Label,
                IsOpen = doorRecord.IsOpen,
                IsLocked = doorRecord.IsLocked
            });
        }

        [HttpGet]
        [Route("{doorId}")]
        public async Task<DoorModel> GetDoorById([FromRoute] [Required] string doorId)
        {
            var doorRecord = await _doorRepositoryService.GetDoor(doorId);

            return (doorRecord == null)
                ? null
                : new DoorModel()
                {
                    Id = doorRecord.Id,
                    Label = doorRecord.Label,
                    IsOpen = doorRecord.IsOpen,
                    IsLocked = doorRecord.IsLocked
                };
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDoorModel model)
        {
            var result = await _doorRepositoryService.AddDoor(new DoorRecordDto()
            {
                IsLocked = model.IsLocked,
                IsOpen = model.IsOpen,
                Label = model.Label
            });

            await _notificationHubContext.Clients.All.NotifyDoorAdded(result);
            return Created($"~api/door/{result.Id}", result);
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] DoorModel model)
        {
            var result = await _doorRepositoryService.UpdateDoor(new DoorRecordDto()
            {
                Id = model.Id,
                IsLocked = model.IsLocked,
                IsOpen = model.IsOpen,
                Label = model.Label
            });

            await _notificationHubContext.Clients.All.NotifyDoorUpdated(result);
            return Ok(result);
        }

        [HttpDelete("{doorId}")]
        public async Task<IActionResult> Delete([FromRoute][Required] string doorId)
        {
            var result = await _doorRepositoryService.RemoveDoor(doorId);
            _logger.LogInformation($"DoorController.Delete DoorId {doorId}");
            return Ok(result.Id);
        }
    }
}
