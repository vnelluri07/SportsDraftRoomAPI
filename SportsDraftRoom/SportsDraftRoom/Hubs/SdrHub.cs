using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SportsDraftRoom.Hubs;

/// <summary>
/// Creating a SignalR hub for chat functionality to test and understand the SignalR library.
/// </summary>

//TODO: will need to add authorization later
//[Authorize]
public class SdrHub : Hub
{
    //TODO: Things to figure out...
    // Admin module/interface for managing teams, auction items, and draft room settings.
    // How to handle user connections and disconnections.
    // Undo a pick or round??


    private readonly IDraftService _draftService;
    public SdrHub(IDraftService draftService)
    {
        _draftService = draftService;
    }

    //Any client can call this method to send a message to all connected clients
    public Task SendMessage(string user, string message)
    {
        return Clients.All.SendAsync("ReceiveMessage", user, message);
    }

    /// <summary>
    /// WHY THE CRAP IS THE OTHER HUB NOT WORKING?
    /// </summary>
    ///TODO: Do I want this in this file or the HubServices file?
    /// 
    private List<TeamsDto> _teams = new();
    private List<AuctionItemsDto> _auctionItems = new();
    private List<AuctionItemsDto> _UpdatedAuctionItems = new();
    private Dictionary<int, string> _connectedUsersInfo = new();
    private bool firstItemPickedInDraft = false;
    private int _totalRounds;

    //Draft room methods
    public async Task InitializeDraftRoomAsync(string teamId, string userName)
    {
        try
        {
            var cancellationToken = new CancellationToken();

            var isParsed = int.TryParse(teamId, out int parsedTeamId);
            if (!isParsed)
            {
                await Clients.Caller.SendAsync("Error", "Invalid team ID format.");
                return;
            }

            var teams = await _draftService.GetTeamsAsync(cancellationToken);
            _teams = teams.ToList();
            var auctionItems = await _draftService.GetAuctionItemsAsync(cancellationToken);
            _auctionItems = auctionItems.ToList();
            var isValidTeam = _teams.Any(t => t.Id == parsedTeamId);

            if (isValidTeam)
            {
                if (!_connectedUsersInfo.ContainsKey(parsedTeamId))
                {
                    _connectedUsersInfo.Add(parsedTeamId, userName);
                }
            }

            var _totalRounds = GetTotalRounds();

            //else
            //TODO: return error..?? maybe..how..

            var isTeamInitialConnection = _connectedUsersInfo.ContainsKey(parsedTeamId);
            if (isTeamInitialConnection)
                await Clients.Caller.SendAsync("DraftRoomInitialized", _teams, _auctionItems);

            //TODO: Else aithe Client lost connection to hub, handle auto pick
            //(thinking out loud, If I don't get a response from the client within a certain time frame,
            //I have auto-pick for them on the hub/backend and send them the updated draft room info)
        }
        catch (Exception ex)
        {
            //TODO: Add proper logging here
            Console.WriteLine($"Error in InitializeDraftRoomAsync: {ex.Message}");
            await Clients.Caller.SendAsync("Error", "An unexpected error occurred while initializing the draft room.");
        }
    }


    // Team picking an auction/draft item
    public async Task SaveTeamAuctionInfo(int teamId, int auctionId, string userName)
    {
        var cancellationToken = new CancellationToken();

        await _draftService.SaveTeamAuctionInfo(new TeamAuctionItemInfo
        {
            TeamId = teamId,
            AuctionItemId = auctionId,
            CreatedBy = userName
        }, cancellationToken);

        if (!firstItemPickedInDraft)
            firstItemPickedInDraft = true;

        _UpdatedAuctionItems.RemoveAll(ai => ai.Id == auctionId);

        // Notify all clients in the draft room about the updated auction item
        await Clients.All.SendAsync("AuctionItemPicked", teamId, auctionId); 
    }


    private int GetTotalRounds()
    {
        int totalrounds;
        if (_auctionItems.Count == 0)
            throw new ArgumentException("Auction items list cannot be empty.", nameof(_auctionItems));

        if (_teams.Count == 0)
            throw new ArgumentException("Teams list cannot be empty.", nameof(_teams));

        if (_auctionItems.Count % _teams.Count == 0)
        {
            totalrounds = _auctionItems.Count / _teams.Count;
        }
        else
        {
            totalrounds = (int)Math.Floor((double)_auctionItems.Count / _teams.Count);
        }
        return totalrounds;
    }
}
