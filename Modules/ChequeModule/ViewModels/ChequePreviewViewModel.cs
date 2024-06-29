using Microsoft.Identity.Client.NativeInterop;
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

        if (chequeEventArgs.TryGetValue(nameof(Payee), out dynamic? payee)) 
        {
            Payee = payee;
        }

        if (chequeEventArgs.TryGetValue(nameof(Amount), out dynamic? amount))
        {
            Amount = (double)amount;
        }

        if (chequeEventArgs.TryGetValue(nameof(AmountInWords), out dynamic? amountInWords))
        {
            AmountInWords = amountInWords;
        }

        if (chequeEventArgs.TryGetValue(nameof(DateCreated), out dynamic? dateCreated))
        {
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
