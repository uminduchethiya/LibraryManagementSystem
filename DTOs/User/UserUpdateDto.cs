using System.ComponentModel.DataAnnotations;

namespace LibraryManagementSystem.DTOs.User
{
    public class UserUpdateDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string Role { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}