using Microsoft.AspNetCore.Mvc;
using Strictly.Api.Extensions;
using Strictly.Application.Reminders;
using Strictly.Domain.Models.Reminders.CreateReminder;

namespace Strictly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RemindersController : ControllerBase
    {
        private readonly IReminderService _reminderService;

        public RemindersController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateReminderRequest createReminderRequest)
        {
            var controllerResponse = (await _reminderService.CreateReminder(createReminderRequest)).FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }
        
        [HttpPost("{reminderId}")]
        public async Task<IActionResult> SendReminder([FromRoute] Guid reminderId)
        {
            var controllerResponse = (await _reminderService.SendReminder(reminderId)).FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }

    }
}
