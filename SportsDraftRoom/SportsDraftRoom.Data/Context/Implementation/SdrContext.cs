using Microsoft.EntityFrameworkCore.Storage;
using SportsDraftRoom.Data.Context;

public class SdrContext : DbContext, ISdrContext
{
    public SdrContext(DbContextOptions<SdrContext> options) : base(options) { }

    public DbSet<Team> Teams => Set<Team>();
    public DbSet<AuctionItem> AuctionItems => Set<AuctionItem>();
    public DbSet<TeamAuctionItem> TeamAuctionItems => Set<TeamAuctionItem>();


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}
