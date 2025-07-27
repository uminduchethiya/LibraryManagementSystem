using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Business.Interfaces;
using LibraryManagementSystem.Repositories.Interfaces;
using SQLitePCL;

namespace LibraryManagementSystem.Business.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repository;
    public BookService(IBookRepository repository)
    {
        _repository = repository;
    }

    public async Task<Book> CreateBookAsync(BookCreatDto dto, int userId)
    {
        var book = new Book
        {
            UserId = userId,
            Title = dto.Title,
            Author = dto.Author,
            Description = dto.Description,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IsDeleted = false
        };
        return await _repository.CreateBookAsync(book);
    }

    public async Task<List<BookDto>> GetAllBooksAsync()
    {
        var books = await _repository.GetAllBooksAsync();
        return books.Select(b => new BookDto
        {
            Id = b.Id,
            UserId = b.UserId,
            Title = b.Title,
            Author = b.Author,
            Description = b.Description
        }).ToList();
    }


    public async Task<Book?> UpdateBookAsync(int id, BookUpdateDto dto)
    {
        var existingBook = await _repository.GetBookByIdAsync(id);
        if (existingBook == null || existingBook.IsDeleted) return null;
        existingBook.Title = dto.Title;
        existingBook.Author = dto.Author;
        existingBook.Description = dto.Description;
        existingBook.UpdatedAt = DateTime.UtcNow;

        return await _repository.UpdateBookAsync(existingBook);
    }
    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _repository.GetBookByIdAsync(id);
        if (book == null || book.IsDeleted)
            return false;

        book.IsDeleted = true;
        book.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateBookAsync(book);
        return true;
    }
}