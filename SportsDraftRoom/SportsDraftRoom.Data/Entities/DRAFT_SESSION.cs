namespace SportsDraftRoom.Data.Entities;
public class DraftSession
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    //Maybe
    //public string? Status { get; set; }
    [Column("IS_ACTIVE")]
    public bool IsActive { get; set; }
    public int PickStyleId { get; set; }
    [Column("CREATED_BY")]
    public int CreatedBy { get; set; }
    [Column("CREATED_DATE")]
    public DateTime CreatedDate { get; set; }
    [Column("MODIFIED_BY")]
    public int ModifiedBy { get; set; }
    [Column("MODIFIED_DATE")]
    public DateTime ModifiedDate { get; set; }

    [ForeignKey(nameof(PickStyleId))]
    public PickStyle? PickStyle { get; set; }
    //TODO: Add later when user stuff is ready
    //[ForeignKey(nameof(CreatedBy))]
    //public User? USer { get; set; }
}
