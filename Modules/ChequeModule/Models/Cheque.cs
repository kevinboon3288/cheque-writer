namespace ChequeWriter.Modules.ChequeModule.Models;

public class Cheque
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Amount { get; set; }
    public DateTime DateCreated { get; set; }

    public Cheque(string name, double amount)
    {
        Name = name;
        Amount = amount;
        DateCreated = DateTime.Now;
    }
}
