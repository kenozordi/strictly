using Microsoft.AspNetCore.Mvc;
using Strictly.Api.Extensions;
using Strictly.Application.Streaks;
using Strictly.Domain.Models.Streaks;

namespace Strictly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreaksController : ControllerBase
    {
        private readonly IStreakService _streakService;

        public StreaksController(IStreakService streakService)
        {
            _streakService = streakService;
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetStreakByUserIdAsync([FromRoute] Guid userId)
        {
            var controllerResponse = (await _streakService.GetStreakByUserIdAsync(userId)).FormatResponse();
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
