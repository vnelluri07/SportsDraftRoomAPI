namespace SportsDraftRoom.Shared.Models;
public class AllTeamsAndAuctionItems
{
    public IEnumerable<TeamsDto> Teams { get; set; }
    public IEnumerable<AuctionItemsDto> AuctionItems { get; set; }
}
