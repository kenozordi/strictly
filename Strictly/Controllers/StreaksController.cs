using Microsoft.AspNetCore.Mvc;
using Strictly.Application.Streaks;
using Strictly.Domain.Models.Streak;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            var (statusCode, response) = await _streakService.GetStreakByUserIdAsync(userId);
            return StatusCode(statusCode, response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStreakRequest createStreakRequest)
        {
            var (statusCode, response) = await _streakService.CreateStreak(createStreakRequest);
            return StatusCode(statusCode, response);
        }

    }
}
