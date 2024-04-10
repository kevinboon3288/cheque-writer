using System;
using System.Configuration;

namespace ChequeWriter.DbInitializer.Console
{
    public class Program
    {
        private const string ConnectionStringName = "cheque-writer-ui";

        public static void Main(string[] args)
        {
            string? connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;

            DbInitializer.Seed(connectionString);
        }
    }
}
