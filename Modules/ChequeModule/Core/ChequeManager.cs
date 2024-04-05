using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChequeWriter.Modules.ChequeModule.Core;

public class ChequeManager : IChequeManager
{
    private List<Models.Cheque> _cheques = new();

    public List<Models.Cheque> Cheques => _cheques;

    public Models.Cheque? GetCheque(int id)
    {
        return _cheques.Any(x => x.Id == id) ? _cheques[id] : null;
    }

    public void AddCheque(Models.Cheque cheque)
    {
        if (!_cheques.Any(x => x.Id == cheque.Id))
        {
            _cheques.Add(cheque);
        }
    }

    public void UpdateCheque(Models.Cheque cheque)
    {
        Models.Cheque? selectedCheque = _cheques.FirstOrDefault(x => x.Id == cheque.Id);

        if (selectedCheque != null)
        {
            selectedCheque.Name = cheque.Name;
            selectedCheque.Amount = selectedCheque.Amount;
            selectedCheque.DateCreated = cheque.DateCreated;
        }
    }

    public void DeleteCheque(int id)
    {
        Models.Cheque? selectedCheque = _cheques.FirstOrDefault(x => x.Id == id);

        if (selectedCheque != null)
        {
            _cheques.Remove(selectedCheque);
        }
    }
}
