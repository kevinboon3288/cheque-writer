using System.ComponentModel.DataAnnotations;

namespace ChequeWriter.DatabaseInitializer.Models
{
    public class Cheque
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
