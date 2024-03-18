using System;

namespace ChequeWriter.Modules.ChequeModule.Models
{
    public class Cheque
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime DateCreated { get; set; }

        public string AmountInWord { get; set; } = string.Empty;

        public Cheque(string name, double amt)
        {
            Name = name;
            Amount = amt;
        }
    }
}
