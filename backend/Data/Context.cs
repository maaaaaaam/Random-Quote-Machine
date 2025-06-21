using Microsoft.EntityFrameworkCore;

public class Context : DbContext
{

    public Context(DbContextOptions<Context> options) : base(options) { }

    public DbSet<Quote> Quotes { get; set; }

}