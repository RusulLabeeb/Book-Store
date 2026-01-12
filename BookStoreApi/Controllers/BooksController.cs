using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("/[controller]")]
public class BooksController : ControllerBase
{

    private readonly List<string> _allowedCategories = new List<string>()
    {
        "Novel",
        "Documentary"
    };

    private static List<Book> Books = new()
    {
        new Book() { Id = 1, Title = "Xyz", Category = "Novel" },
        new Book() { Id = 2, Title = "Abc", Category = "Documentary" },
    };


    // GET /books
    [HttpGet]
    public ActionResult<List<Book>> Get()
    {
        return Ok(Books);
    }
    
    // POST /books
    [HttpPost]
    public ActionResult CreateBook([FromBody] BookRequest request)
    {
        // Duplicate title check (case-insensitive)
        // Option 1
        var titleExists = Books.Any(b =>
            b.Title.Equals(request.Title, StringComparison.OrdinalIgnoreCase));
        // Option 2 is to check lower cases of both the input and what we have with ==.
        

        if (titleExists)
            return BadRequest("A book with the same title already exists.");

        // Category validation
        if (!_allowedCategories.Contains(request.Category))
            return BadRequest("Category is invalid.");

        var newId = Books.Max(b => b.Id) + 1;

        var newBook = new Book
        {
            Id = newId,
            Title = request.Title,
            Category = request.Category
        };

        Books.Add(newBook);

        return Created($"/books/{newId}", null);
    }

    // GET /books/{id}
    [HttpGet("{id:int}")]
    public ActionResult<Book> GetSingleBook(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound();
        return Ok(book);
    }

    // PUT /books/{id}
    [HttpPut("{id:int}")]
    public ActionResult UpdateBook(int id, [FromBody] BookRequest request)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);

        if (book is null)
            return NotFound();

        if (!_allowedCategories.Contains(request.Category))
            return BadRequest("Category is invalid.");

        book.Title = request.Title;
        book.Category = request.Category;

        return NoContent();
    }
    
    // GET /books/category/{category}
    [HttpGet("category/{category}")]
    public ActionResult<List<Book>> GetByCategory(string category)
    {
        var result = Books
            .Where(b => b.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public ActionResult DeleteBook(int id)
    {
        var book = Books.FirstOrDefault(b => b.Id == id);
        if (book is null)
            return NotFound();
        Books.Remove(book);
        return NoContent();
    }

}
