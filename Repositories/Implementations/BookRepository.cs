using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Repositories.Implementations;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;
    public BookRepository(AppDbContext context)
    {
        _context = context;
    }
    // Adds a new book to the database.
    public async Task<Book> CreateBookAsync(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book;
    }

    //Retrieves all non-deleted books from the database.
    public async Task<List<Book>> GetAllBooksAsync()
    {
        return await _context.Books
        .Where(b => !b.IsDeleted).ToListAsync();
    }

    // Fetches a single book by its ID
    public async Task<Book?> GetBookByIdAsync(int id)
    {
        return await _context.Books.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
    }
    //  Updates an existing book in the database
    public async Task<Book> UpdateBookAsync(Book book)
    {
        _context.Books.Update(book);
        await _context.SaveChangesAsync();
        return book;
    }

    // deletes a book
    public async Task<bool> DeleteBookAsync(int id)
    {
        var book = await _context.Books.FirstOrDefaultAsync(b => b.Id == id && !b.IsDeleted);
        if (book == null) return false;

        book.IsDeleted = true;
        book.UpdatedAt = DateTime.UtcNow;

        _context.Books.Update(book);
        await _context.SaveChangesAsync();

        return true;
    }

}