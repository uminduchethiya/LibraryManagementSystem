using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Repositories.Interfaces;

public interface IBookRepository
{
    Task<Book> CreateBookAsync(Book book);
    Task<List<Book>> GetAllBooksAsync();
    Task<Book?> GetBookByIdAsync(int id);
    Task<Book> UpdateBookAsync(Book book);
    Task<bool> DeleteBookAsync(int id);

}