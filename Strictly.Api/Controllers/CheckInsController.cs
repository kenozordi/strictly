using Microsoft.AspNetCore.Mvc;
using Strictly.Api.Extensions;
using Strictly.Application.CheckIns;
using Strictly.Domain.Models.CheckIns.CheckIntoStreak;
using Strictly.Domain.Models.CheckIns.CreateCheckIn;

namespace Strictly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckInsController : ControllerBase
    {
        private readonly ICheckInService _checkInService;

        public CheckInsController(ICheckInService checkInService)
        {
            _checkInService = checkInService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCheckInRequest createCheckInRequest)
        {
            var controllerResponse = (await _checkInService.CreateCheckIn(createCheckInRequest))
                .FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }
        
        [HttpPatch("{checkInId}/user/{userId}")]
        public async Task<IActionResult> CheckIn([FromRoute] Guid checkInId, [FromRoute] Guid userId)
        {
            var controllerResponse = (await _checkInService.CheckIn(new CheckInRequest() { UserId = userId, CheckInId = checkInId}))
                .FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }
        
        [HttpGet("today/user/{userId}")]
        public async Task<IActionResult> GetCheckInForToday([FromRoute] Guid userId)
        {
            var controllerResponse = (await _checkInService.GetCheckInForToday(userId))
                .FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }


    }
}
