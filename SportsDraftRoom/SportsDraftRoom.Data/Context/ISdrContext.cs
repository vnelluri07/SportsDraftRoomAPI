namespace SportsDraftRoom.Data.Context;
public interface ISdrContext
{
    DbSet<Team> Teams { get; }
    DbSet<AuctionItem> AuctionItems { get; }
}
