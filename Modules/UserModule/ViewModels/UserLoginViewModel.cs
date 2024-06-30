using CommonModule.ValidationRules;

namespace ChequeWriter.Modules.UserModule.ViewModels;

public class UserLoginViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IUserManager _userManager;
    private List<UserLevel> _userLevels = new();
    private UserLevel? _selectedUserLevel = new();
    private UserInputParameter? _userName;
    private string? _password;
    private bool _isVisible;

    public UserInputParameter? UserName 
    {
        get { return _userName; }
        set { SetProperty(ref _userName, value); } 
    }

    public string? Password
    {
        get { return _password; }
        set { SetProperty(ref _password, value); }
    }

    public bool IsVisible
    {
        get { return _isVisible; }
        set { SetProperty(ref _isVisible, value); }
    }

    public UserLevel? SelectedUserLevel
    {
        get { return _selectedUserLevel; }
        set { SetProperty(ref _selectedUserLevel, value); }
    }

    public List<UserLevel> UserLevels
    {
        get { return _userLevels; }
        set { SetProperty(ref _userLevels, value); }
    }

    public DelegateCommand PasswordDisplayCommand { get; private set; }
    public DelegateCommand LoginCommand { get; private set; }

    public event EventHandler? ClearLoginPasswordReceived;

    public UserLoginViewModel(IRegionManager regionManager,IEventAggregator eventAggregator, IUserManager userManager)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _userManager = userManager;

        LoginCommand = new DelegateCommand(OnLogin);
        PasswordDisplayCommand = new DelegateCommand(OnPasswordDisplay);

        OnRefresh();
    }

    private void OnClearPasswordReceived()
    {
        ClearLoginPasswordReceived?.Invoke(this, new EventArgs());
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        OnRefresh();
    }

    private void OnRefresh() 
    {
        UserName = new UserInputParameter();
        Password = String.Empty;
        IsVisible = false;
        SelectedUserLevel = new();
        UserLevels = _userManager.GetUserLevels();

        OnClearPasswordReceived();
    }

    private void OnLogin()
    {
        if (UserName == null || string.IsNullOrEmpty(Password) || _selectedUserLevel is null || UserName.InputValue == null) 
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Invalid username, password or authority.");
            return;
        }

        if (!_userManager.IsValidUser(UserName.InputValue, Password, SelectedUserLevel!.Id)) 
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Unauthorized user. Please re-enter the username and password.");
            return;
        }

        IRegion region = _regionManager.Regions["UserContentRegion"];
        region.RequestNavigate("MainView");

        int? userId = _userManager.GetCurrentUserId(UserName.InputValue, Password, _selectedUserLevel.Id);
        if (userId != null) 
        {
            _eventAggregator.GetEvent<CurrentUserEvent>().Publish(new Dictionary<string, dynamic>()
            {
                { "UserNoId", userId.Value },
                { "SelectedUserLevelId", _selectedUserLevel.Id  }
            });        
        }
        else 
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish($"Unable to find the user with {UserName.InputValue}");
        }

        _eventAggregator.GetEvent<NotificationEvent>().Publish("Login successfully");
    }

    private void OnPasswordDisplay() 
    {
        IsVisible = !_isVisible;
    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
        return true;
    }

    public void OnNavigatedFrom(NavigationContext navigationContext)
    {
    }

    public class UserInputParameter: UserInputNotifyDataErrorInfo
    {
        private string? _inputValue;

        public string? InputValue 
        {
            get { return _inputValue; }
            set 
            { 
                SetProperty(ref _inputValue, value);
                if (value != null) 
                {
                    Validate(nameof(InputValue), value);
                }
            }
        }
    }
}


