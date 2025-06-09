namespace SportsDraftRoom.Data.Context.Implementation;

public class SdrContext : DbContext, ISdrContext
{
    /*
     *When you configure your DbContext using AddDbContext,
     * you are essentially setting up how the DbContext should be created and configured,
     * often including details like the database provider and connection string. Entity Framework Core (EF Core)
     * relies on Dependency Injection (DI) to inject this configuration into your DbContext when it's needed.
     */
    public SdrContext(DbContextOptions<SdrContext> options) : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<AuctionItem> AuctionItems => Set<AuctionItem>();
}
