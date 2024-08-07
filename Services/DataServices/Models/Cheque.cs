﻿namespace DataServices.Models;

public class Cheque
{
    [Key]
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Amount { get; set; }
    public DateTime? DateCreated { get; set; }
    public User? User { get; set; }

    [ForeignKey("User")]
    public int UserId { get; set; }
}
