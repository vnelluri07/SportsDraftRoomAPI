namespace SportsDraftRoom.Hubs;

public class SdrHub2 : Hub
{
    private readonly IDraftService _draftService;
    public SdrHub2(IDraftService draftService)
    {
        _draftService = draftService;
    }

    //Variables per draft room
    private List<TeamsDto> _teams = new();
    private List<AuctionItemsDto> _auctionItems = new();
    private Dictionary<int, string> _connectedUsersInfo = new();

    public async Task InitializeDraftRoomAsync(int teamId, string userName, CancellationToken cancellationToken)
    {
        var teams = await _draftService.GetTeamsAsync(cancellationToken);
        _teams = teams.ToList();
        var auctionItems = await _draftService.GetAuctionItemsAsync(cancellationToken);
        _auctionItems = auctionItems.ToList();

        var isValidTeam = _teams.Any(t => t.Id == teamId);

        if (isValidTeam)
            _connectedUsersInfo.Add(teamId, userName); 
        //else
        //TODO: return error..?? maybe..how..

        var isTeamInitialConnection = _connectedUsersInfo.ContainsKey(teamId);
        if (isTeamInitialConnection)
            await Clients.Caller.SendAsync("DraftRoomInitialized", _teams, _auctionItems, cancellationToken);
        //TODO: Else aithe Client lost connection to hub, handle auto pick
        //(thinking out loud, If I don't get a response from the client within a certain time frame,
        //I have auto-pick for them on the hub/backend and send them the updated draft room info)
    }

    // Team picking an auction/draft item
    public async Task SaveTeamAuctionInfo(int teamId, int auctionId,
        CancellationToken cancellationToken)
    {
        await _draftService.SaveTeamAuctionInfo(new TeamAuctionItemInfo
        {
            TeamId = teamId,
            AuctionItemId = auctionId,
            CreatedBy = "TEST MANN" //I want to record if the user has picked the player or if the Auto pick has picked it
        }, cancellationToken);

        _auctionItems.RemoveAll(ai => ai.Id == auctionId);

        await Clients.All.SendAsync("AuctionItemPicked", teamId, auctionId, cancellationToken);
    }

}
