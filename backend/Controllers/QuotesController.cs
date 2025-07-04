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

    private static readonly Random random = new();

    [HttpGet]
    public async Task<ActionResult<Quote>> GetRandomQuote()
    {
        var count = await _context.Quotes.CountAsync();

        int index = random.Next(count);

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


    //  POST importing quotes query

    // This config is used in the both POST queries via CsvQuoteListParser.Parse()
    private readonly static CsvHelper.Configuration.CsvConfiguration csvConfig = new(System.Globalization.CultureInfo.InvariantCulture) { HasHeaderRecord = false };

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

        await _context.Quotes.AddRangeAsync(quotes);
        await _context.SaveChangesAsync();

        return Ok(new { message = $"{quotes.Count} quote(s) imported." });
    }

    //  POST reinitializing the table query

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

        using (var transaction = await _context.Database.BeginTransactionAsync())
        {

            try
            {
                _context.Quotes.RemoveRange(_context.Quotes);
                await _context.SaveChangesAsync();


                await _context.Quotes.AddRangeAsync(quotes);
                await _context.SaveChangesAsync();

                await transaction.CommitAsync();
                return Ok(new { message = "The table is reinitialized" });
            }
            catch (Exception err)
            {
                await transaction.RollbackAsync();
                return BadRequest(new { message = $"Reinitialization failed: {err.Message}" });
            }

        }

    }
}