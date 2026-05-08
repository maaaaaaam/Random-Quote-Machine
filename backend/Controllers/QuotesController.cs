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

    //  GET random query
    [HttpGet]
    public async Task<ActionResult<Quote>> GetRandomQuote()
    {

        var count = await _context.Quotes.CountAsync();

        if (count == 0) return NotFound( new { message = "No quotes found"});

        int index = Random.Shared.Next(count);

        var quote = await _context.Quotes.Skip(index).FirstOrDefaultAsync();

        return Ok(quote);
    }

    //  GET all the table query
    [HttpGet("all")]
    public async Task<ActionResult<List<Quote>>> GetAllQuotes()
    {
        List<Quote> quotes;

        try
        {
            quotes = await _context.Quotes.ToListAsync();
            return quotes;
        }
        catch (Exception err)
        {
            return BadRequest(new { message = $"Fetching all the quotes failed: {err.Message}" });
        }
    }



    // This config is used in the both POST queries below in CsvQuoteListParser.Parse()
    private readonly static CsvHelper.Configuration.CsvConfiguration csvConfig = new(System.Globalization.CultureInfo.InvariantCulture) { HasHeaderRecord = false };

    // POST importing quotes query
    [HttpPost]
    public async Task<IActionResult> ImportQuotes(IFormFile file)
    {
        if (file == null || file.Length == 0) return BadRequest(new { message = "Empty or no file." });

        List<Quote> quotes;

        using (var stream = file.OpenReadStream())
        {
            try
            {
                quotes = CsvQuoteListParser.Parse(stream, csvConfig);
            }
            catch (Exception err)
            {
                return BadRequest(new { message = $"File reading error: {err.Message}" });
            }
        }

        try
        {
            _context.Quotes.AddRange(quotes);
            await _context.SaveChangesAsync();
        } catch (Exception err)
        {
            return BadRequest(new { message = $"Adding quotes error: {err.Message}" });
        }

        return Ok(new { message = $"{quotes.Count} quote(s) imported." });
    }

    // POST reinitializing the table query
    [HttpPost("reinit")]
    public async Task<IActionResult> ReinitializeTable()
    {

        List<Quote> quotes;

        using (var stream = System.IO.File.OpenRead("10InitialQuotesForReinit.csv"))
        {
            try
            {
                quotes = CsvQuoteListParser.Parse(stream, csvConfig);
            }
            catch (Exception err)
            {
                return BadRequest(new { message = $"Reinitialization error: {err.Message}" });
            }
        }

        try
        {
            _context.Quotes.RemoveRange(_context.Quotes);
            _context.Quotes.AddRange(quotes);
            await _context.SaveChangesAsync();

            return Ok(new { message = "The table is reinitialized" });
        }
        catch (Exception err)
        {
            return BadRequest(new { message = $"Reinitialization failed: {err.Message}" });
        }

    }
}