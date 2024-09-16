namespace ChequeWriter.Modules.ChequeModule.ViewModels;

public class ChequeFormViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IChequeManager _chequeManager;

    private string? _dateCreated;
    private string? _payee;
    private string? _amount;

    public string? DateCreated
    {
        get { return _dateCreated; }
        set 
        { 
            SetProperty(ref _dateCreated, value);
            if (string.IsNullOrEmpty(value)) 
            {
                _dateCreated = DateTime.Now.ToString("dd-MMM-yyyy", CultureInfo.InvariantCulture);
            }
            PublishToPreview("DateCreated", _dateCreated!);
        }
    }
    public string? Payee
    {
        get { return _payee; }
        set 
        { 
            SetProperty(ref _payee, value);
            if (value != null)
            {
                PublishToPreview("Payee", _payee!);
            }
        }
    }
    public string? Amount
    {
        get { return _amount; }
        set 
        { 
            SetProperty(ref _amount, value);
            if (value != null)
            {
                PublishToPreview("Amount", _amount!);
            }
        }
    }

    public DelegateCommand SaveChequeCommand { get; set; }
    public DelegateCommand CancelCommand { get; set; }
    public DelegateCommand ResetCommand { get; set; }

    public ChequeFormViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IChequeManager chequeManager)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _chequeManager = chequeManager;

        SaveChequeCommand = new DelegateCommand(OnSaveCheque);
        CancelCommand = new DelegateCommand(OnCancel);
        ResetCommand = new DelegateCommand(OnCancel);
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
    }

    private void PublishToPreview(string attribute, string value) 
    {
        _eventAggregator.GetEvent<PreviewChequeEvent>().Publish(new Dictionary<string, dynamic>() 
        {
            { attribute, value }
        });
    }

    private void OnSaveCheque() 
    {
        if (string.IsNullOrEmpty(Payee))
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Please provide a payee.");
            return;
        }
        if (string.IsNullOrEmpty(Amount))
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Please provide an amount.");
            return;
        }
        //if (!DateTime.TryParseExact(DateCreated, "dd-MMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime inputDate)) 
        //{
        //    _eventAggregator.GetEvent<NotificationEvent>().Publish("Unable to date time properly");
        //    return;
        //}
        if (!double.TryParse(Amount, out double amountValue))
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Unable to convert amount to double when save a new cheque.");
            return;
        }

        //TODO: Need to get a current user.
        _chequeManager.AddCheque(Payee, amountValue, DateTime.Now, 1);
    }

    private void OnCancel() 
    {
        IRegion region = _regionManager.Regions["UserContentRegion"];
        region.RequestNavigate("MainView");
    }

    private void OnReset()
    {
        OnRefresh();
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
