
using LibraryManagementSystem.Business.Interfaces;
using LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagementSystem.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;
    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }
    // create new book
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] BookCreatDto dto)
    {

        var userIdClaim = User.FindFirst("id");
        if (userIdClaim == null) return Unauthorized("User ID claim missing in token");
        int userId = int.Parse(userIdClaim.Value);
        try
        {
            var book = await _bookService.CreateBookAsync(dto, userId);
            return Ok(book);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the book: {ex.Message}");
        }
    }

    // get all book
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        try
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while creating the book: {ex.Message}");
        }
    }

    // update bookd
    [Authorize(Roles = "Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookUpdateDto dto)
    {
        var updatedBook = await _bookService.UpdateBookAsync(id, dto);
        if (updatedBook == null)
            return NotFound("Book not found.");

        return Ok(updatedBook);


    }

    // delete book
    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result) return NotFound(new { message = "Book not found or already deleted." });

            return Ok(new { message = "Book deleted successfully." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while deleting the book: {ex.Message}");
        }
    }





}
