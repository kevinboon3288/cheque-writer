namespace ChequeWriter.Modules.ChequeModule.Core;

public interface IChequeManager
{
    List<Cheque> Cheques { get; }

    void AddCheque(Cheque cheque);
    void DeleteCheque(int id);
    Cheque? GetChequeById(int id);
    void UpdateCheque(Cheque cheque);
}