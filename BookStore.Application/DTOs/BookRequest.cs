using System.ComponentModel.DataAnnotations;
using BookStore.Application.ValidationsAndAttributes;

namespace BookStore.Application.DTOs;

public class BookRequest
{
    public string Title { get; set; }
    [AllowedStrings(["Novel", "Documentary"])]
    public string Category { get; set; }
}