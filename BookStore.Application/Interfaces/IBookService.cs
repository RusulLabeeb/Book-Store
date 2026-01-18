using BookStore.Application.DTOs;
using BookStore.Domain.Entities;

namespace BookStore.Application.Interfaces;

public interface IBookService
{
    Book CreateBook(BookRequest request);
    Book? GetById(int id);
    List<Book> GetBooks();
    bool DeleteBook(int id);
}