using System.Security.Cryptography;
using System.Text;
using LibraryManagementSystem.Business.Interfaces;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interfaces;
using SQLitePCL;

namespace LibraryManagementSystem.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;
        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        // Register New User  
        public async Task<bool> RegisterAsync(User user, string password)
        {
            var existing = await _userRepo.GetByEmailAsync(user.Email);
            if (existing != null) return false;
            user.PasswordHash = HashPassword(password);
            await _userRepo.AddUserAsync(user);
            return true;
        }
       
        // Authenticate user campare with the provide credentials.
        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepo.GetByEmailAsync(email);
            if (user == null || user.PasswordHash != HashPassword(password))
                return null;
            return user;
        }

        // Hashes the provided password using SHA256 algorithm.
        private string HashPassword(string password)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
      
        // Retrieves all users
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepo.GetAllUsersAsync();
        }

        // Updates an existing user's information by ID.
        public async Task<bool> UpdateUserAsync(int id, User updatedUser)
        {
            var existingUser = await _userRepo.GetByIdAsync(id);
            if (existingUser == null) return false;

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.Role = updatedUser.Role;
            existingUser.IsActive = updatedUser.IsActive;
            existingUser.UpdatedAt = DateTime.UtcNow;

            await _userRepo.UpdateUserAsync(existingUser);
            return true;

        }
       
        // Deletes a user by ID.
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepo.DeleteUserAsync(user);
            return true;
        }
      
        // Retrieves a user by their.
        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepo.GetByIdAsync(id);
        }


    }
}