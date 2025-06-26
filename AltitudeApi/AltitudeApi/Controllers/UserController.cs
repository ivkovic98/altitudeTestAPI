using Altitude.Bussiness.Interface;
using Altitude.Bussiness.Interfaces;
using Altitude.Bussiness.Models;
using Altitude.Bussiness.Models.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AltitudeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IImageUploadService _imageUploadService;

        public UserController(IUserService userService, IImageUploadService imageUploadService)
        {
            _userService = userService;
            _imageUploadService = imageUploadService;
        }

        [HttpGet("get-all")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserResponseModel>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponseModel>> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpGet("email/{email}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponseModel>> GetUserByEmail(string email)
        {
            var user = await _userService.GetUserByEmailAsync(email);
            return user is not null ? Ok(user) : NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUser([FromBody] UserRegisterModel model)
        {
            await _userService.AddUserAsync(model);
            return Ok("User successfully created.");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserRegisterModel model)
        {
            await _userService.UpdateUserAsync(id, model);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            await _userService.DeleteUserAsync(id);
            return NoContent();
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId)) return Unauthorized();

            var profile = await _userService.GetCurrentUserProfileAsync(Guid.Parse(userId));
            return profile is not null ? Ok(profile) : NotFound();
        }

        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage(UpdateUserPhotoModel model)
        {
            if (model.ProfilePhoto == null || model.ProfilePhoto.Length == 0)
                return BadRequest("Fajl nije validan.");

            var success = await _userService.UpdateUserProfileImageAsync(model);
            if (!success)
                return StatusCode(StatusCodes.Status500InternalServerError, "Image upload failed or user not found.");

            var user = await _userService.GetUserByIdAsync(Guid.Parse(model.UserId));
            return Ok(new { imageUrl = user?.ProfileImageUrl });
        }
    }
}