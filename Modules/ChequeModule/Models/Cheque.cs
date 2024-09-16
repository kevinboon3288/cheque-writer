namespace ChequeWriter.Modules.ChequeModule.Models;

public class Cheque
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public required double Amount { get; set; }
    public required DateTime? DateCreated { get; set; } 
}
