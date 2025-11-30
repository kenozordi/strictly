using Microsoft.AspNetCore.Mvc;
using Strictly.Api.Extensions;
using Strictly.Application.CheckIns;

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

        [HttpGet("streak/{streakId}")]
        public async Task<IActionResult> GetCheckInHistory([FromRoute] Guid streakId)
        {
            var controllerResponse = (await _checkInService.GetCheckInHistory(streakId)).FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }


    }
}
