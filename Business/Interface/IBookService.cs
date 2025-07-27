using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Business.Interfaces;

public interface IBookService
{
    Task<Book> CreateBookAsync(BookCreatDto dto, int userId);
    Task<List<BookDto>> GetAllBooksAsync();
    Task<Book?> UpdateBookAsync(int id, BookUpdateDto dto);
    Task<bool> DeleteBookAsync(int id);
}