namespace ChequeWriter.Modules.ChequeModule.Core;

public class ChequeManager : IChequeManager
{
    private List<Cheque> _cheques = new();

    public List<Cheque> Cheques => _cheques;

    public Cheque? GetCheque(int id)
    {
        return _cheques.Any(x => x.Id == id) ? _cheques[id] : null;
    }

    public void AddCheque(Cheque cheque)
    {
        if (!_cheques.Any(x => x.Id == cheque.Id))
        {
            _cheques.Add(cheque);
        }
    }

    public void UpdateCheque(Cheque cheque)
    {
        Cheque? selectedCheque = _cheques.FirstOrDefault(x => x.Id == cheque.Id);

        if (selectedCheque != null)
        {
            selectedCheque.Name = cheque.Name;
            selectedCheque.Amount = selectedCheque.Amount;
            selectedCheque.DateCreated = cheque.DateCreated;
        }
    }

    public void DeleteCheque(int id)
    {
        Cheque? selectedCheque = _cheques.FirstOrDefault(x => x.Id == id);

        if (selectedCheque != null)
        {
            _cheques.Remove(selectedCheque);
        }
    }
}
