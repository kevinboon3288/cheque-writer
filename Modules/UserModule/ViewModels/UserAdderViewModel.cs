namespace ChequeWriter.Modules.UserModule.ViewModels;

public class UserAdderViewModel: BindableBase, INavigationAware
{
    private readonly IRegionManager _regionManager;
    private readonly IEventAggregator _eventAggregator;
    private readonly IUserManager _userManager;
    private List<UserLevel> _userLevels = new();
    private UserLevel? _selectedUserLevel = new();
    private NewUserInputParameter _userName = new();
    private NewUserInputParameter _jobTitle = new();
    private string? _password;
    private bool _isVisible;
    private int? _currentUserId;

    public NewUserInputParameter UserName
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

    public NewUserInputParameter JobTitle
    {
        get { return _jobTitle; }
        set { SetProperty(ref _jobTitle, value); }
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
    public DelegateCommand CancelCommand {  get; private set; }
    public DelegateCommand AddUserCommand { get; private set; }

    public event EventHandler? ClearAdderPasswordReceived;

    public UserAdderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IUserManager userManager)
    {
        _regionManager = regionManager;
        _eventAggregator = eventAggregator;
        _userManager = userManager;

        PasswordDisplayCommand = new DelegateCommand(OnPasswordDisplay);
        CancelCommand = new DelegateCommand(OnReturn);
        AddUserCommand = new DelegateCommand(OnAddUser);
    }

    private void OnClearPasswordReceived()
    {
        ClearAdderPasswordReceived?.Invoke(this, new EventArgs());
    }

    public void OnNavigatedTo(NavigationContext navigationContext)
    {
        _eventAggregator.GetEvent<HeaderTitleUIControlEvent>().Publish("Add User");

        if (navigationContext.Parameters.ContainsKey("currentUserId"))
        {
            _currentUserId = navigationContext.Parameters.GetValue<int>("currentUserId");
        }

        OnRefresh();
    }

    private void OnRefresh()
    {
        UserName = new NewUserInputParameter();
        JobTitle = new NewUserInputParameter();
        Password = String.Empty;
        IsVisible = false;
        SelectedUserLevel = null;
        UserLevels = _userManager.GetUserLevels();

        OnClearPasswordReceived();
    }

    private void OnReturn() 
    {
        IRegion region = _regionManager.Regions["ModuleContentRegion"];
        region.RequestNavigate("UserManagementView");
    }

    private void OnAddUser()
    {
        if (_currentUserId == null)
        {
            throw new ArgumentNullException($"Empty current user is defined in {nameof(OnAddUser)}");
        }

        if (string.IsNullOrEmpty(UserName.InputValue)) 
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Please enter a new user name.");
            return;
        }

        if (string.IsNullOrEmpty(Password))
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Please set a password.");
            return;
        }

        if (string.IsNullOrEmpty(JobTitle.InputValue))
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Please provide a job title.");
            return;
        }

        if (SelectedUserLevel == null) 
        {
            _eventAggregator.GetEvent<NotificationEvent>().Publish("Please register an authority for the new user.");
            return;
        }

        if (!_userManager.IsExistUser(UserName.InputValue, SelectedUserLevel.Id))
        {
            _userManager.AddUser(UserName.InputValue, Password, JobTitle.InputValue, SelectedUserLevel.Id, _currentUserId.Value);

            IRegion region = _regionManager.Regions["ModuleContentRegion"];
            region.RequestNavigate("UserManagementView");            
        }
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

    public class NewUserInputParameter : UserInputNotifyDataErrorInfo
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
