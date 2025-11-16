using Microsoft.AspNetCore.Mvc;
using Strictly.Api.Factories;
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
            var response = await _streakService.GetStreakByUserIdAsync(userId);
            return ResponseFactory.ToObjectResult(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateStreakRequest createStreakRequest)
        {
            var response = await _streakService.CreateStreak(createStreakRequest);
            return ResponseFactory.ToObjectResult(response);
        }

    }
}
