
namespace ChequeWriter.Modules.ChequeModule.ViewModels;

public class ChequeViewModel : BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IChequeManager _chequeManager;
    private readonly IEventAggregator _eventAggregator;

    private string? _payeeName;
    private string? _amount;
    private DateOnly? _date;
    private string? _drawer;

    public string? PayeeName
    {
        get { return _payeeName; }
        set { SetProperty(ref _payeeName, value); }
    }

    public string? Amount
    {
        get { return _amount; }
        set { SetProperty(ref _amount, value); }
    }

    public DateOnly? Date
    {
        get { return _date; }
        set { SetProperty(ref _date, value); }
    }

    public string? Drawer
    {
        get { return _drawer; }
        set { SetProperty(ref _drawer, value); }
    }

    public DelegateCommand NavigateBack { get; set; }
    public DelegateCommand CancelCommand { get; private set; }
    public DelegateCommand SaveFormCommand { get; private set; }

    public ChequeViewModel(IRegionManager regionManager, IChequeManager chequeManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _chequeManager = chequeManager;
        _eventAggregator = eventAggregator;

        NavigateBack = new DelegateCommand(OnReturn);
        CancelCommand = new DelegateCommand(OnReturn);
        SaveFormCommand = new DelegateCommand(OnSaveForm);

    }

    private void OnReturn()
    {
        IRegion region = _regionManager.Regions["UserContentRegion"];
        region.RequestNavigate("MainView");
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        _eventAggregator.GetEvent<UIControlEvent>().Publish("Cheque");
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    private void OnSaveForm()
    {

    }
}
