namespace SportsDraftRoom.Data.Entities;
[Table("TEAM_AUCTION_ITEM")]
public class TeamAuctionItem
{
    public int Id { get; set; }
    [Column("TEAM_ID")]
    public int TeamId { get; set; }
    [Column("AUCTION_ITEM_ID")]
    public int AuctionItemId { get; set; }
    [Column("IS_ACTIVE")]
    public bool IsActive { get; set; }

    [Column("CREATED_BY")]
    public int CreatedBy { get; set; }

    [Column("CREATED_DATE")] public DateTime CreatedDate { get; set; } = DateTime.MinValue;
    [Column("MODIFIED_BY")]
    public int ModifiedBy { get; set; }

    [Column("MODIFIED_DATE")] public DateTime ModifiedDate { get; set; } = DateTime.MinValue;

    [ForeignKey(nameof(TeamId))]
    public Team? Team { get; set; }
    [ForeignKey(nameof(AuctionItemId))]
    public AuctionItem? AuctionItem { get; set; }
}
