namespace ChequeWriter.Modules.ChequeModule.Core;

public class ChequeManager : IChequeManager
{
    private readonly IDataService _dataService;
    

    public ChequeManager(IDataService dataService)
    {
        _dataService = dataService;
    }

    public Cheque? GetChequeById(int id)
    {
        var cheque = _dataService.GetChequeById(id);

        return new Cheque() { Name = cheque!.Name!, Amount = cheque.Amount, Id = cheque.Id, DateCreated = cheque.DateCreated };
    }

    public void AddCheque(Cheque cheque)
    {
        _dataService.AddCheque(cheque);
    }

    public void UpdateCheque(Cheque cheque)
    {
        _dataService.UpdateCheque(cheque);
    }

    public void DeleteCheque(int id)
    {
        Cheque? selectedCheque = GetChequeById(id);

        if (selectedCheque != null)
        {
            _cheques.Remove(selectedCheque);
        }
    }

    public static string TranslateToWord(double amount)
    {
        return AmountUtils.Convert(amount);
    }
}
