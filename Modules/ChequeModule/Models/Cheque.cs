namespace ChequeWriter.Modules.ChequeModule.Models;

public class Cheque
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required double Amount { get; set; }
    public required DateTime DateCreated { get; set; }

    public Cheque(string name, double amount)
    {
        Name = name;
        Amount = amount;
        DateCreated = DateTime.UtcNow;
    }
}
