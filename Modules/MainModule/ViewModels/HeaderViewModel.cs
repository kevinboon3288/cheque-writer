

namespace ChequeWriter.Modules.MainModule.ViewModels;

public class HeaderViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private string? _title;
    private bool _isAccessible;
    private bool _isRadioChecked;

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

    public bool IsRadioChecked
    {
        get { return _isRadioChecked; }
        set 
        { SetProperty(ref _isRadioChecked, value); }
    }

    public DelegateCommand NavigateCommand { get; private set; }

    public DelegateCommand LogOutCommand { get; private set; }

    public HeaderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;

        NavigateCommand = new DelegateCommand(OnNavigateToUserManagement);
        LogOutCommand = new DelegateCommand(OnLogOut);

        _eventAggregator.GetEvent<UIControlEvent>().Subscribe(OnTitleChanged);
        _eventAggregator.GetEvent<CurrentUserEvent>().Subscribe(OnCurrentUserLevel);
    }

    private void OnNavigateToUserManagement() 
    {
        IsRadioChecked = false;

        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate("UserManagementView");
    }

    private void OnLogOut()
    {
        IsAccessible = false;
        IsRadioChecked = false;
        Title = String.Empty;

        IRegion region = _regionManager.Regions["UserContentRegion"];
        region.RequestNavigate("UserLoginView");
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
