namespace ChequeWriter.Modules.ChequeModule.Core;

public class ChequeManager : IChequeManager
{
    private readonly IDataService _dataService;
    

    public ChequeManager(IDataService dataService)
    {
        _dataService = dataService;
    }

    public List<Cheque> GetAllCheques() 
    {
        List<Cheque> cheques = new List<Cheque>();

        foreach (var cheque in _dataService.GetAllCheques()!) 
        {
            cheques.Add(new Cheque() 
            { 
                Id = cheque.Id,
                Name = cheque.Name,
                Amount = cheque.Amount,
                DateCreated = cheque.DateCreated
            });
        }

        return cheques;
    }

    public Cheque? GetChequeById(int id)
    {
        var cheque = _dataService.GetChequeById(id);

        return new Cheque() { Name = cheque!.Name!, Amount = cheque.Amount, Id = cheque.Id, DateCreated = cheque.DateCreated };
    }

    public void AddCheque(string name, double amount, DateTime? dateCreated, int userId)
    {
        int result = _dataService.AddCheque(name, amount, dateCreated, userId);
    }

    public void DeleteCheque(int id)
    {
        _dataService.DeleteCheque(id);
    }

    public static string TranslateToWord(double amount)
    {
        return AmountUtils.Convert(amount);
    }
}
