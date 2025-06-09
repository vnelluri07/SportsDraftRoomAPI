namespace SportsDraftRoom.Data.Entities;

[Table("AUCTION_ITEM")]
public class AuctionItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool IsActive  { get; set; }
    public string? Picture { get; set; }
    public int CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public int ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
}
