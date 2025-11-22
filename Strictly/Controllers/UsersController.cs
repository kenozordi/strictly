using Microsoft.AspNetCore.Mvc;
using Strictly.Application.Users;

namespace Strictly.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var (statusCode, data) = await _userService.GetAllAsync();
            return StatusCode(statusCode, data);
        }

    }
}
