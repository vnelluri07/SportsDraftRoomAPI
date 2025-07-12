using System.Reflection.Metadata.Ecma335;

namespace SportsDraftRoom.Data.Entities;
public class PickStyle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string? Name { get; set; }
    [Column("IS_ACTIVE")]
    public bool IsActive { get; set; }

}
