using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{

    //  Hardcoded Quotes
    //
    // public class Quote {
    //     public required string Text { get; set; }
    //     public required string Author { get; set; }
    // }
    //
    // private static readonly Quote[] quotes = {
    //     new Quote { Text = "Don't touch it if it works", Author = "A programmer"},
    //     new Quote { Text = "Quote 2", Author = "Author 2"},
    //     new Quote{ Text = "Quote 3", Author = "Author 3"}
    // };
    //
    // private static readonly Random random = new();
    //
    // [HttpGet]
    // public IActionResult GetRandomQuote()
    // {
    //     var quote = quotes[random.Next(quotes.Length)];
    //     return Ok(quote);
    // }

    private readonly Context _context;

    public QuotesController(Context context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<Quote>> GetRandomQuote()
    {
        var count = await _context.Quotes.CountAsync();

        var random = new Random();
        int index = random.Next(count);

        var quote = await _context.Quotes.Skip(index).FirstOrDefaultAsync();

        return Ok(quote);
    }
}