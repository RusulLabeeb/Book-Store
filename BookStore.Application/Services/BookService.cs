using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Application.Services;

public class BookService(IBookStoreDbContext dbContext) : IBookService
{
    public Book CreateBook(BookRequest request)
    {
        var newBook = new Book()
        {
            Title = request.Title,
            AuthorId = request.AuthorId.Value
        };
        dbContext.Books.Add(newBook);
        dbContext.SaveChanges();
        return newBook;
    }
    
    public List<BookDto> GetBooks()
    {
        var books = dbContext.Books
            .AsNoTracking()
            .Include(b => b.Author)
            .Select(b => new BookDto()
            {
                Id = b.Id,
                Title = b.Title,
                Author = new AuthorDto(){Name = b.Author.Name, Id = b.Author.Id }
            })
            .ToList();
        return books;
    }

    public Book? GetById(int id)
    {
        throw new NotImplementedException();
    }


    public bool DeleteBook(int id)
    {
        var book = dbContext.Books.FirstOrDefault(b => b.Id == id);
        if (book is null)
            return false;
        dbContext.SaveChanges();
        return true;
    }
}