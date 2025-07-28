using LibraryManagementSystem.Business.Interfaces;
using LibraryManagementSystem.Constants;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using LibraryManagementSystem.DTOs.User;



namespace LibraryManagementSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        public AuthController(IUserService userService, IConfiguration configuration, ITokenService tokenService)
        {
            _userService = userService;
            _configuration = configuration;
            _tokenService = tokenService;

        }
        // Registers a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            try
            {
                var user = new User
                {
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Role = dto.Role ?? UserRole.User,
                    PasswordHash = string.Empty
                };

                var result = await _userService.RegisterAsync(user, dto.Password);
                if (!result) return BadRequest("User already exists.");

                return Ok("Registration successful.");
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while processing your request.",
                });
            }

        }

        // Authenticates the user and returns a JWT token
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto dto)
        {
            try
            {
                var user = await _userService.AuthenticateAsync(dto.Email, dto.Password);
                if (user == null) return Unauthorized("Invalid credentials.");

                var token = _tokenService.GenerateToken(user);
                return Ok(new
                {
                    message = "Login successful",
                    token,
                    user = new
                    {
                        user.Id,
                        user.FirstName,
                        user.LastName,
                        user.Email,
                        user.Role
                    }
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while processing your request.",
                });
            }

        }

        // Retrieves a list of all registered users
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsersAsync();
                return Ok(users);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while retrieving users."
                });
            }
        }

        //    Updates the specified user's information
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserUpdateDto dto)
        {
            try
            {
                var userRole = User.FindFirstValue(ClaimTypes.Role);

                if (userRole != "Admin")
                    return Forbid("Only admin can update users");

                var updatedUser = new User
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Email = dto.Email,
                    Role = dto.Role,
                    IsActive = dto.IsActive,
                    PasswordHash = ""  
                };

                var result = await _userService.UpdateUserAsync(id, updatedUser);
                if (!result)
                    return NotFound("User not found.");

                return Ok("User updated successfully.");
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while updating the user."
                });
            }
        }

        // Deletes a user by ID
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null) return NotFound("User not Found");

                var result = await _userService.DeleteUserAsync(id);
                if (!result)
                {
                    return BadRequest("Failed to delete user");
                }

                return Ok("User deleted successfully");
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An error occurred while deleting the user."
                });
            }
        }






    }


}
