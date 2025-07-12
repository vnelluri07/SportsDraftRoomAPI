using SportsDraftRoom.Repo;

namespace SportsDraftRoom.Service.Implementation;
public class DraftService : IDraftService
{

    private readonly IDraftRepo _draftRepo;

    public DraftService(IDraftRepo draftRepo)
    {
        _draftRepo = draftRepo;
    }

    public async Task<IEnumerable<TeamsDto>> GetTeamsAsync(CancellationToken cancellationToken)
    {
        return await _draftRepo.GetTeamsAsync(cancellationToken);
    }

    public async Task<IEnumerable<AuctionItemsDto>> GetAuctionItemsAsync(CancellationToken cancellationToken)
    {
        return await _draftRepo.GetAuctionItemsAsync(cancellationToken);
    }

    public async Task SaveTeamAuctionInfo(TeamAuctionItemInfo teamAuctionItemInfo, CancellationToken cancellationToken)
    {
        await _draftRepo.SaveTeamAuctionInfo(teamAuctionItemInfo, cancellationToken);
    }
}
