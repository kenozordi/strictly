using Microsoft.AspNetCore.Mvc;
using Strictly.Application.Interfaces;
using Strictly.Application.Services;
using Strictly.Application.Streaks;
using Strictly.Domain.Models.Entities;
using Strictly.Domain.Models.Streak;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Strictly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreakController : ControllerBase
    {
        private readonly IStreakService _streakService;

        public StreakController(IStreakService streakService)
        {
            _streakService = streakService;
        }

        // GET: api/<StreakController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/<StreakController>
        [HttpPost]
        public async Task<string> Post([FromBody] CreateStreakRequest createStreakRequest)
        {
            var (isSuccess, message, data) = await _streakService.CreateStreak(createStreakRequest);
            return message;
        }

    }
}
