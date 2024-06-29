using System.Globalization;

namespace ChequeWriter.Modules.ChequeModule.ViewModels;

public class ChequePreviewViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;

    private string? _dateCreated;
    private string? _payee;
    private double? _amount;
    private string? _amountInWords;

    public string? DateCreated
    {
        get { return _dateCreated; }
        set { SetProperty(ref _dateCreated, value); }
    }

    public string? Payee
    {
        get { return _payee; }
        set { SetProperty(ref _payee, value); }
    }

    public double? Amount
    {
        get { return _amount; }
        set { SetProperty(ref _amount, value); }
    }

    public string? AmountInWords
    {
        get { return _amountInWords; }
        set { SetProperty(ref _amountInWords, value); }
    }

    public ChequePreviewViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;

        _eventAggregator.GetEvent<PreviewChequeEvent>().Subscribe(OnChequeInfoReceived);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        OnRefresh();
    }

    private void OnRefresh() 
    {
        DateCreated = null;
        Payee = null;
        Amount = null;
        AmountInWords = null;
    }

    private void OnChequeInfoReceived(Dictionary<string, dynamic> chequeEventArgs) 
    {
        if (chequeEventArgs == null) 
        {
            return;
        }

        if (chequeEventArgs.TryGetValue("Payee", out dynamic? payee) &&
            chequeEventArgs.TryGetValue("Amount", out dynamic? amount) &&
            chequeEventArgs.TryGetValue("AmountInWords", out dynamic? amountInWords) &&
            chequeEventArgs.TryGetValue("DateCreated", out dynamic? dateCreated)) 
        {
            Payee = payee;
            Amount = (double)amount;
            AmountInWords = amountInWords;
            DateCreated = dateCreated;
        }
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
