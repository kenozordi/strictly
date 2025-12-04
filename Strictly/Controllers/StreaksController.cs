using Microsoft.AspNetCore.Mvc;
using Strictly.Api.Extensions;
using Strictly.Application.CheckIns;
using Strictly.Application.Streaks;
using Strictly.Domain.Models.Streaks;

namespace Strictly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreaksController : ControllerBase
    {
        private readonly IStreakService _streakService;
        private readonly ICheckInService _checkInService;

        public StreaksController(IStreakService streakService, ICheckInService checkInService)
        {
            _streakService = streakService;
            _checkInService = checkInService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetStreakByUserIdAsync([FromRoute] Guid userId)
        {
            var controllerResponse = (await _streakService.GetStreakByUserIdAsync(userId)).FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }

        [HttpGet("{streakId}/check-ins")]
        public async Task<IActionResult> GetCheckInHistory([FromRoute] Guid streakId)
        {
            var controllerResponse = (await _checkInService.GetActiveCheckInSchedule(streakId)).FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStreakRequest createStreakRequest)
        {
            var controllerResponse = (await _streakService.CreateStreak(createStreakRequest)).FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }

    }
}
