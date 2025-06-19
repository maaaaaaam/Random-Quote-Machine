using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase {

    public class Quote {
        public required string Text { get; set; }
        public required string Author { get; set; }
    }

    private static readonly Quote[] quotes = {
        new Quote { Text = "Don't touch it if it works", Author = "A programmer"},
        new Quote { Text = "Quote 2", Author = "Author 2"},
        new Quote{ Text = "Quote 3", Author = "Author 3"}
    };

    private static readonly Random random = new();

    [HttpGet]
    public IActionResult GetRandomQuote()
    {
        var quote = quotes[random.Next(quotes.Length)];
        return Ok(quote);
    }
}