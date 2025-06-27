public static class CsvQuoteListParser
{
    public static List<Quote> Parse(IFormFile file, CsvHelper.Configuration.CsvConfiguration csvConfig)
    {

        List<Quote> quotes;

        using (var reader = new StreamReader(file.OpenReadStream()))
        using (var csv = new CsvHelper.CsvReader(reader, csvConfig))
        {
            csv.Context.RegisterClassMap<QuoteMap>();

            quotes = csv.GetRecords<Quote>().ToList();
        }

        return quotes;

    }
}