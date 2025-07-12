namespace SportsDraftRoom.Data.Entities;

[Table("TEAM")]
public class Team
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    [Column("IS_ACTIVE")]
    public bool IsActive { get; set; }
    public int Budget { get; set; }
    [Column("CREATED_BY")]
    public int CreatedBy { get; set; }
    [Column("CREATED_DATE")]
    public DateTime CreatedDate { get; set; }
    [Column("MODIFIED_BY")]
    public int ModifiedBy { get; set; }
    [Column("MODIFIED_DATE")]
    public DateTime ModifiedDate { get; set; }
}
