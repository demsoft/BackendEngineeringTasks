using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Application.Services;
using BackendEngineeringTasks.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BackendEngineeringTasks.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public UsersController(IUserAppService userAppService)
        {
            _userAppService = userAppService;   
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userAppService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userAppService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserDto userDto)
        {
            try
            {
                var createdUser = await _userAppService.CreateUserAsync(userDto);
                return CreatedAtAction(nameof(GetUserById), new { userId = createdUser.Id }, createdUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateProject(int userId, UserDto userDto)
        {
            try
            {
                await _userAppService.UpdateUserAsync(userId, userDto);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            try
            {
                await _userAppService.DeleteUserAsync(userId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
