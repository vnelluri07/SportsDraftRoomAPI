namespace SportsDraftRoom.Data.Entities;
public  class DraftSessionDetail
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int TeamId { get; set; }
    [Column("CREATED_BY")]
    public int CreatedBy { get; set; }
    [Column("CREATED_DATE")]
    public DateTime CreatedDate { get; set; }
    [Column("MODIFIED_BY")]
    public int ModifiedBy { get; set; }
    [Column("MODIFIED_DATE")]
    public DateTime ModifiedDate { get; set; }
}
