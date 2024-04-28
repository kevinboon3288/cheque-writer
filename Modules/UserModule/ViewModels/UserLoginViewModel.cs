using ChequeWriter.Modules.UserModule.Core;

namespace ChequeWriter.Modules.UserModule.ViewModels
{
    public class UserLoginViewModel: BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserManager _userManager;
        private List<UserLevel> _userLevels = new();
        private UserLevel? _selectedUserLevel = new();
        private string? _userName;
        private string? _password;

        public string? UserName 
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); } 
        }

        public string? Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
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

        public DelegateCommand LoginCommand { get; set; }

        public UserLoginViewModel(IRegionManager regionManager,IEventAggregator eventAggregator, IUserManager userManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _userManager = userManager;

            LoginCommand = new DelegateCommand(OnLogin);

            OnRefresh();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OnRefresh();
        }

        private void OnRefresh() 
        {
            UserName = String.Empty;
            Password = String.Empty;
            SelectedUserLevel = new();
            UserLevels = _userManager.GetUserLevels();
        }

        private void OnLogin()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || _selectedUserLevel is null) 
            {
                throw new ArgumentNullException("Empty username or password.");
            }

            if (_userManager.IsValidUser(UserName, Password, SelectedUserLevel!.Id)) 
            {
                IRegion region = _regionManager.Regions["UserContentRegion"];
                region.RequestNavigate("MainView");               

                _eventAggregator.GetEvent<CurrentUserEvent>().Publish(_selectedUserLevel.Id);
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
}
