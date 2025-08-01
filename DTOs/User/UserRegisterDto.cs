using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Constants;
namespace LibraryManagementSystem.DTOs.User
{
    public class UserRegisterDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [MinLength(8)]
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = UserRole.User; 
    }
}
