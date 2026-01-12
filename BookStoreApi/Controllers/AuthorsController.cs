using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi.Controllers;

[ApiController]
[Route("/authors")]
public class AuthorsController : ControllerBase
{
    public static List<Author> Authors = new()
    {
        new Author { Id = 1, Name = "George Orwell" },
        new Author { Id = 2, Name = "Yuval Noah Harari" }
    };

    // GET /authors
    [HttpGet]
    public ActionResult<List<Author>> GetAll()
    {
        return Ok(Authors);
    }

    // GET /authors/{id}
    [HttpGet("{id:int}")]
    public ActionResult<Author> GetById(int id)
    {
        var author = Authors.FirstOrDefault(a => a.Id == id);

        if (author is null)
            return NotFound();

        return Ok(author);
    }

    // POST /authors
    [HttpPost]
    public ActionResult Create([FromBody] AuthorRequest request)
    {
        var newId = Authors.Max(a => a.Id) + 1;

        var author = new Author
        {
            Id = newId,
            Name = request.Name
        };

        Authors.Add(author);

        return Created($"/authors/{newId}", null);
    }

    // DELETE /authors/{id}
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var author = Authors.FirstOrDefault(a => a.Id == id);

        if (author is null)
            return NotFound();

        Authors.Remove(author);

        return NoContent();
    }
}