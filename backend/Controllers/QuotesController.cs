using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class QuotesController : ControllerBase
{

    //  Hardcoded Quotes API logic
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
    

    //  GET logic

    private static readonly Random random = new();

    [HttpGet]
    public async Task<ActionResult<Quote>> GetRandomQuote()
    {
        var count = await _context.Quotes.CountAsync();

        int index = random.Next(count);

        var quote = await _context.Quotes.Skip(index).FirstOrDefaultAsync();

        return Ok(quote);
    }


    //  POST logic

    private readonly static CsvHelper.Configuration.CsvConfiguration csvConfig = new(System.Globalization.CultureInfo.InvariantCulture) { HasHeaderRecord = false };

    [HttpPost]
    public async Task<IActionResult> ImportQuotes(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest(new { message = "Empty or no file." });
           
        var quotes = new List<Quote>();

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvHelper.CsvReader(reader, csvConfig))
        {
            csv.Context.RegisterClassMap<QuoteMap>();

            try
            {
                quotes = csv.GetRecords<Quote>().ToList();
            }
            catch (Exception e)
            {
                return BadRequest(new { message = $"File reading error: {e.Message}" });
            }

        }

        await _context.Quotes.AddRangeAsync(quotes);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"{quotes.Count} quote(s) imported." });
    }
}