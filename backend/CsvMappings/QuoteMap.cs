using CsvHelper.Configuration;


public class QuoteMap : ClassMap<Quote>
{
    public QuoteMap()
    {
        Map(m => m.Text).Index(0);
        Map(m => m.Author).Index(1);
    }
}