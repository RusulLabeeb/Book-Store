using System.ComponentModel.DataAnnotations;
using BookStore.Domain.Entities.Common;

namespace BookStore.Domain.Entities;

public class Loan : BaseEntity
{
    public int UserId { get; set; }
    public User User { get; set; }
    
    public int BookId { get; set; }
    public Book Book { get; set; }
    
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}
