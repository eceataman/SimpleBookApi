using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private static List<Book> _books = new()
    {
        new Book { Id = 1, Title = "1984", Author = "George Orwell", Year = 1949 },
        new Book { Id = 2, Title = "Dune", Author = "Frank Herbert", Year = 1965 }
    };

    private readonly BookUpdateDtoValidator _updateValidator = new();
    private readonly IdValidator _idValidator = new();

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var validation = _idValidator.Validate(id);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);

        var book = _books.FirstOrDefault(x => x.Id == id);
        if (book == null)
            return NotFound();

        var dto = new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            Year = book.Year
        };

        return Ok(dto);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] BookUpdateDto dto)
    {
        var validation = _updateValidator.Validate(dto);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);

        if (id != dto.Id)
            return BadRequest("ID mismatch");

        var book = _books.FirstOrDefault(x => x.Id == id);
        if (book == null)
            return NotFound();

        book.Title = dto.Title;
        book.Author = dto.Author;
        book.Year = dto.Year;

        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var validation = _idValidator.Validate(id);
        if (!validation.IsValid)
            return BadRequest(validation.Errors);

        var book = _books.FirstOrDefault(x => x.Id == id);
        if (book == null)
            return NotFound();

        _books.Remove(book);
        return NoContent();
    }
}
