namespace ChequeWriter.Modules.UserModule.ViewModels;

public class UserManagementViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IUserManager _userManager;
    private readonly IEventAggregator _eventAggregator;
    private List<User> _users = new();
    private User _selectedUser = new();
    private int? _currentUserId;

    public List<User> Users 
    {
        get { return _users; } 
        set {  SetProperty(ref _users, value); }
    }

    public User SelectedUser
    {
        get { return _selectedUser; }
        set { SetProperty(ref _selectedUser, value); }
    }

    public DelegateCommand DeleteUserCommand { get; private set; }
    public DelegateCommand NavigateToUserAdderCommand { get; private set; }
    public DelegateCommand NavigateBack { get; private set; }

    public UserManagementViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IUserManager userManager)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _userManager = userManager;

        NavigateBack = new DelegateCommand(OnReturn);
        NavigateToUserAdderCommand = new DelegateCommand(OnNavigateToUserAdder);
        DeleteUserCommand = new DelegateCommand(OnDeleteUser);
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        _eventAggregator.GetEvent<UIControlEvent>().Publish("User Management");

        if (navigationContext.Parameters.ContainsKey("currentUserId")) 
        {
            _currentUserId = navigationContext.Parameters.GetValue<int>("currentUserId");
        }

        OnRefresh();
    }

    private void OnRefresh() 
    {
        Users = _userManager.GetAllUsers();

        if (_currentUserId != null) 
        {
            User? currentUser = Users.FirstOrDefault(x => x.Id == _currentUserId);
            if (currentUser != null)
            { 
                currentUser.IsEnabled = false; 
            }
        }
    }

    private void OnReturn()
    {
        IRegion region = _regionManager.Regions["UserContentRegion"];
        region.RequestNavigate("MainView");
    }

    private void OnNavigateToUserAdder()
    {
        if (_currentUserId == null)
        {
            throw new ArgumentNullException($"Empty or null current user.");
        }

        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate($"UserAdderView?currentUserId={_currentUserId}");
    }

    private void OnDeleteUser() 
    {
        foreach (User user in Users) 
        {
            if (user.IsChecked) 
            {
                _userManager.DeletUser(user);
            }
        }

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
