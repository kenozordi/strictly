using Microsoft.AspNetCore.Mvc;
using Strictly.Api.Extensions;
using Strictly.Application.CheckIns;
using Strictly.Application.Streaks;

namespace Strictly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreakParticipantsController : ControllerBase
    {
        private readonly IStreakService _streakService;
        private readonly ICheckInService _checkInService;

        public StreakParticipantsController(IStreakService streakService, ICheckInService checkInService)
        {
            _streakService = streakService;
            _checkInService = checkInService;
        }

        [HttpPatch("/api/streaks/{streakId}/participants/{userId}")]
        public async Task<IActionResult> Post([FromRoute] Guid streakId, [FromRoute] Guid userId)
        {
            var controllerResponse = (await _streakService.AddStreakParticipant(streakId, userId)).FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }

    }
}
