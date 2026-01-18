using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Entities.Common;

namespace BookStore.Domain.Entities;

public class Book : BaseEntity
{
    [MaxLength(200)]
    public string Title { get; set; }
    
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    
    public ICollection<BookGenre> Genres { get; set; }
    
}