public static class CsvQuoteListParser
{
    public static List<Quote> Parse(Stream stream, CsvHelper.Configuration.CsvConfiguration csvConfig)
    {

        List<Quote> quotes;

        using (var reader = new StreamReader(stream))
        using (var csv = new CsvHelper.CsvReader(reader, csvConfig))
        {
            csv.Context.RegisterClassMap<QuoteMap>();

            quotes = csv.GetRecords<Quote>().ToList();
        }

        return quotes;

    }
}