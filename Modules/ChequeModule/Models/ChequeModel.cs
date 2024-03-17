using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.Modules.ChequeModule.Models
{
    public class ChequeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime DateCreated { get; set; }

        public string AmountInWord { get; set; }

        public ChequeModel(string name, string amtInWord)
        {
            Name = name;
            AmountInWord = amtInWord;
        }
    }
}
