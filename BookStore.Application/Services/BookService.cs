using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Domain.Entities;

namespace BookStore.Application.Services;

public class BookService(IBookStoreDbContext dbContext) : IBookService
{
    public Book CreateBook(BookRequest request)
    {
        var newBook = new Book()
        {
            Title = request.Title
        };
        dbContext.Books.Add(newBook);
        dbContext.SaveChanges();
        return newBook;
    }
    
    public List<Book> GetBooks()
    {
        var books = dbContext.Books.ToList();
        return books;
    }

    public Book? GetById(int id)
    {
        throw new NotImplementedException();
    }


    public bool DeleteBook(int id)
    {
        throw new NotImplementedException();
    }
}