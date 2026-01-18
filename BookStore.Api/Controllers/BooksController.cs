using BookStore.Application.DTOs;
using BookStore.Application.Interfaces;
using BookStore.Application.Services;
using BookStore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers;

[ApiController]
[Route("/[controller]")]
public class BooksController(IBookService bookService) : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Book>> GetBooks()
    {
        var result = bookService.GetBooks();
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public ActionResult<Book?> GetBookById(int id)
    {
        var result = bookService.GetById(id);
        if (result is null)
            return NotFound("Book was not found");
        return Ok(result);
    }

    [HttpPost]
    public ActionResult<Book> CreateBook([FromBody] BookRequest request)
    {
        var result = bookService.CreateBook(request);
        return Ok(result);
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult<Book> DeleteBook(int id)
    {
        var result = bookService.DeleteBook(id);
        if (!result)
            return NotFound();
        return NoContent();
    }
    
}