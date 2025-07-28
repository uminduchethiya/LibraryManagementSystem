using LibraryManagementSystem.Models;
public interface ITokenService
{
    // Genarate Token
        string GenerateToken(User user);
}
