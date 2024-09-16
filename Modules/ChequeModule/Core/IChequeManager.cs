namespace ChequeWriter.Modules.ChequeModule.Core;

public interface IChequeManager
{
    public List<Cheque> GetAllCheques();
    void AddCheque(string name, double amount, DateTime? dateCreated, int userId);
    void DeleteCheque(int id);
    Cheque? GetChequeById(int id);
}