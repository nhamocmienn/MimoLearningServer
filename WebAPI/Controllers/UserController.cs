using DTO.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            var result = await _userService.LoginAsync(dto);
            if (result == null)
                return Unauthorized("Invalid username or password");

            return Ok(result);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequestDto dto)
        {
            var updated = await _userService.UpdateProfileAsync(dto);
            if (updated == null)
                return BadRequest("Không thể cập nhật");

            return Ok(updated);
        }

    }
}
