namespace SportsDraftRoom.Repo;

public interface IDraftRepo
{
    public Task<IEnumerable<TeamsDto>> GetTeamsAsync(CancellationToken cancellationToken);
    public Task<IEnumerable<AuctionItemsDto>> GetAuctionItemsAsync(CancellationToken cancellationToken);
    public Task SaveTeamAuctionInfo(TeamAuctionItemInfo teamAuctionItemInfo, CancellationToken cancellationToken);
}
