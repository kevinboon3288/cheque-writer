namespace DataServices.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Password {  get; set; }
    public string? JobTitle {  get; set; }
    [Required]
    public int CreatedBy {  get; set; }
    public UserLevel? UserLevel { get; set; }
    
    [ForeignKey("UserLevel")]
    public int UserLevelId { get; set; }
}
