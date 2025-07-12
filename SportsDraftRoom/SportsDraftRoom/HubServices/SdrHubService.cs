using SportsDraftRoom.Data.Entities;

namespace SportsDraftRoom.API.HubServices;

public class SdrHubService
{
    private readonly IDraftService _draftService;
    public SdrHubService(IDraftService draftService)
    {
        _draftService = draftService;
    }

    public async Task<AllTeamsAndAuctionItems> InitializeDraftRoomAsync(string teamId, string userName, CancellationToken cancellationToken)
    {
        var allItems = new AllTeamsAndAuctionItems();

        var teams = await _draftService.GetTeamsAsync(cancellationToken);
        
        var auctionItems = await _draftService.GetAuctionItemsAsync(cancellationToken);

        allItems.Teams = teams;
        allItems.AuctionItems = auctionItems;

        return allItems;

    }

}
