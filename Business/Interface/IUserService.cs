using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Business.Interfaces
{
    public interface IUserService
    {
        Task<bool> RegisterAsync(User user, string password);
        Task<User?> AuthenticateAsync(string email, string password);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> UpdateUserAsync(int id, User updatedUser);
        Task<bool> DeleteUserAsync(int id);
        Task<User> GetByIdAsync(int id);
    }
}