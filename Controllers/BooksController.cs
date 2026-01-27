using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("/[controller]")]
public class BooksController : ControllerBase
{

    private readonly List<string> allowedCategories = new List<string>()
    {
        "Novel",
        "Documentary"
    };
    public static List<Book> Books = new()
    {
        new Book() { Id = 1, Title = "Xyz", Category = "Novel" },
        new Book() { Id = 2, Title = "Abc", Category = "Documentary" },
    };

    
    [HttpGet]
public IActionResult GetBooks(string? sortBy, string sortOrder = "asc")
{
    var query = Books.AsQueryable();

    if (!string.IsNullOrEmpty(sortBy))
    {
        if (sortBy.ToLower() == "title")
        {
            query = (sortOrder == "asc") ? query.OrderBy(b => b.Title) : query.OrderByDescending(b => b.Title);
        }
        else if (sortBy.ToLower() == "category")
        {
            query = (sortOrder == "asc") ? query.OrderBy(b => b.Category) : query.OrderByDescending(b => b.Category);
        }
        else if (sortBy.ToLower() == "id")
        {
            query = (sortOrder == "asc") ? query.OrderBy(b => b.Id) : query.OrderByDescending(b => b.Id);
        }
    }

    return Ok(query.ToList());
}
    [HttpPost]
    public ActionResult CreateBook([FromBody] BookRequest request)
    {
        var newId = Books.Max(b => b.Id) + 1;
        if (!allowedCategories.Contains(request.Category))
            return BadRequest("Category is invalid");
        var newBook = new Book()
        {
            Title = request.Title,
            Category = request.Category,
            Id = newId
        };
        Books.Add(newBook);
        return Created();
    }

    [HttpGet("{id:int}")]
    public ActionResult<Book> GetSingleBook(int id)
    {
        return Books[0];
    }

[HttpGet("pagination")]
public IActionResult GetBooks(int page = 1, int pageSize = 10)
{
    var query = Books.AsQueryable();

    var totalCount = query.Count();

    var items = query.Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToList(); 

    var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

   
    return Ok(new 
    {
        Items = items,
        TotalCount = totalCount,
        CurrentPage = page,
        TotalPages = totalPages
    });
}

[HttpGet("search")]
public IActionResult SearchBooks([FromQuery] string query)
{
    if (string.IsNullOrWhiteSpace(query))
    {
        return BadRequest("enter search of text");
    }


    var results = Books
        .Where(b => b.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
        .ToList(); 

    return Ok(results);
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
