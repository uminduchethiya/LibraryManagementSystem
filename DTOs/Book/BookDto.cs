namespace LibraryManagementSystem.DTOs.Book;
using LibraryManagementSystem.Enums;
public class BookDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ISBN { get; set; } = string.Empty;
    public BookType BookType { get; set; }
}