using Microsoft.AspNetCore.Mvc;

namespace BookStoreApi;

    [ApiController]
    [Route("[controller]")]
    public class AuthorsController : ControllerBase
    {
        private static List<Author> Authors = new List<Author>
        {
            new Author { Id = 1, Title = "Agatha Christie",Category = "Novel" },
            new Author { Id = 2, Title = "George Orwell",Category = "Documentary" },
		    new Author { Id = 2, Title = "luma Johne",Category = "Novel" },

        };

        [HttpGet]
        public IActionResult GetAuthors(string? sortBy, string sortOrder = "asc")
        {
            var query = Authors.AsQueryable();

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.ToLower() == "title")
                {
                    query = (sortOrder == "asc") ? query.OrderBy(a => a.Title) : query.OrderByDescending(a => a.Title);
                }
                else if (sortBy.ToLower() == "id")
                {
                    query = (sortOrder == "asc") ? query.OrderBy(a => a.Id) : query.OrderByDescending(a => a.Id);
                }
                else if (sortBy.ToLower() == "Category")
                {
                    query = (sortOrder == "asc") ? query.OrderBy(a => a.Category) : query.OrderByDescending(a => a.Category);
                }
            }

            return Ok(query.ToList());
        }
    [HttpPut("{id}")] 
    public IActionResult UpdateAuthor(int id, [FromBody] AutherRequest updatedData)
    {
        var author = Authors.FirstOrDefault(a => a.Id == id);

        if (author == null)
        {
            return NotFound("NotFound");
        }

        author.Title = updatedData.Name;

        return Ok(author);
    }

    [HttpGet("search")]
    public IActionResult SearchAuthors([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            return BadRequest("	Enter name of Author");
        }

        var results = Authors
            .Where(a => a.Title.Contains(query, StringComparison.OrdinalIgnoreCase))
            .ToList(); 

        return Ok(results);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteAuthor(int id)
    {
        var author = Authors.FirstOrDefault(a => a.Id == id);
        if (author == null) return NotFound();

        Authors.Remove(author); 
        return NoContent(); 
    }
}
