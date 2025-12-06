using Microsoft.AspNetCore.Mvc;
using Strictly.Api.Extensions;
using Strictly.Application.CheckIns;
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
            var controllerResponse = (await _checkInService.CreateCheckIn(createCheckInRequest)).FormatResponse();
            return StatusCode(controllerResponse.HttpStatusCode, controllerResponse.Response);
        }


    }
}
