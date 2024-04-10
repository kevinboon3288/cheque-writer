namespace DataServices.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public UserLevel? UserLevel { get; set; }
    
    [ForeignKey("UserLevel")]
    public int UserLevelId { get; set; }
}
