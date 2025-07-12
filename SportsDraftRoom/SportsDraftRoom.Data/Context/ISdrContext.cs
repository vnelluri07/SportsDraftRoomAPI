using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SportsDraftRoom.Data.Context;
public interface ISdrContext
{
    DbSet<Team> Teams { get; }
    DbSet<AuctionItem> AuctionItems { get; }
    DbSet<TeamAuctionItem> TeamAuctionItems { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
