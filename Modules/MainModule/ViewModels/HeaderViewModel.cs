

namespace ChequeWriter.Modules.MainModule.ViewModels;

public class HeaderViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private string? _title;
    private bool _isAccessible;

    public string? Title 
    {
        get { return _title; }
        set { SetProperty(ref _title, value); }
    }

    public bool IsAccessible
    {
        get { return _isAccessible; }
        set { SetProperty(ref _isAccessible, value); }
    }

    public DelegateCommand NavigateCommand { get; private set; }

    public HeaderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;

        NavigateCommand = new DelegateCommand(OnNavigateToUserManagement);

        _eventAggregator.GetEvent<UIControlEvent>().Subscribe(OnTitleChanged);
        _eventAggregator.GetEvent<CurrentUserEvent>().Subscribe(OnCurrentUserLevel);
    }

    private void OnNavigateToUserManagement() 
    {
        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate("UserManagementView");
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        // Entry Point when navigate to HeaderView
    }

    private void OnTitleChanged(string title) 
    {
        if (!string.IsNullOrEmpty(title)) 
        {
            Title = title;
        }
    }

    private void OnCurrentUserLevel(int currentUserLevel)
    {
        IsAccessible = (currentUserLevel == 1) ? true : false;
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }
}
