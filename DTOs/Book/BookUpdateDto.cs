namespace LibraryManagementSystem.DTOs.Book;

using System.ComponentModel.DataAnnotations;
using LibraryManagementSystem.Enums;
public class BookUpdateDto
{
    [Required]
    [StringLength(100)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [StringLength(50)]
    public string Author { get; set; } = string.Empty;
    [StringLength(500)]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string ISBN { get; set; } = string.Empty;
    [Required]
    public BookType BookType { get; set; }
}
