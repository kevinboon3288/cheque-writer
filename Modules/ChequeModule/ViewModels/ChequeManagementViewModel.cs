namespace ChequeWriter.Modules.ChequeModule.ViewModels;

public class ChequeManagementViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IChequeManager _chequeManager;
    private ObservableCollection<Cheque>? _cheques;

    public ObservableCollection<Cheque>? Cheques
    {
        get { return _cheques; }
        set { SetProperty(ref _cheques, value); }
    }

    public DelegateCommand NavigateBack {  get; private set; }

    public ChequeManagementViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IChequeManager chequeManager)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _chequeManager = chequeManager;

        NavigateBack = new DelegateCommand(OnReturn);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        _eventAggregator.GetEvent<HeaderTitleUIControlEvent>().Publish("Cheque Management");

        OnRefresh();
    }

    private void OnRefresh() 
    {
        Cheques = new ObservableCollection<Cheque>(_chequeManager.Cheques);
    }

    private void OnReturn()
    {
        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate("ChequeView");
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
