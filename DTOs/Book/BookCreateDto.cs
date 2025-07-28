namespace LibraryManagementSystem.DTOs.Book;

using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Enums;
public class BookCreatDto
{
    [Required(ErrorMessage = "Title is Required")]
    [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "Author is required")]
    [StringLength(50, ErrorMessage = "Author name can't exceed 50 characters")]
    public string Author { get; set; } = string.Empty;

    [StringLength(500, ErrorMessage = "Description can't exceed 500 characters")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "ISBN is required")]
    public string ISBN { get; set; } = string.Empty;

    [Required(ErrorMessage = "Book type is required")]
    public BookType BookType { get; set; }

}