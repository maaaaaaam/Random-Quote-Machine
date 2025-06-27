using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/quotes")]
public class QuotesController : ControllerBase
{

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
        
        List<Quote> quotes;

        try
        {
           quotes = CsvQuoteListParser.Parse(file, csvConfig);
        }
        catch (Exception err)
        {
            return BadRequest(new { message = $"File reading error: {err.Message}" });
        }

        await _context.Quotes.AddRangeAsync(quotes);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"{quotes.Count} quote(s) imported." });
    }
}