using System;
using LibraryManagementSystem.Constants;
namespace LibraryManagementSystem.Models;
public class User
{
    public int Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }   
    public required string Role { get; set; } = UserRole.User;
    public required string PasswordHash { get; set; }   
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    
}