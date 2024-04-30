﻿namespace ChequeWriter.Modules.UserModule.ViewModels
{
    public class UserAdderViewModel: BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IUserManager _userManager;
        private List<UserLevel> _userLevels = new();
        private UserLevel? _selectedUserLevel = new();
        private string? _userName;
        private string? _jobTitle;
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

        public string? JobTitle
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
        public DelegateCommand CancelCommand {  get; private set; }
        public DelegateCommand AddUserCommand { get; private set; }


        public UserAdderViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IUserManager userManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _userManager = userManager;

            CancelCommand = new DelegateCommand(OnReturn);
            AddUserCommand = new DelegateCommand(OnAddUser);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            OnRefresh();

            _eventAggregator.GetEvent<UIControlEvent>().Publish("Add User");
        }

        private void OnRefresh()
        {
            UserName = String.Empty;
            Password = String.Empty;
            JobTitle = String.Empty;
            SelectedUserLevel = new();
            UserLevels = _userManager.GetUserLevels();
        }

        private void OnReturn() 
        {
            IRegion region = _regionManager.Regions["ModuleContentRegion"];
            region.RequestNavigate("UserManagementView");
        }

        private void OnAddUser()
        {
            if (SelectedUserLevel != null && !string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password)) 
            {
                _userManager.AddUser(UserName, Password, JobTitle, SelectedUserLevel.Id);

                IRegion region = _regionManager.Regions["ModuleContentRegion"];
                region.RequestNavigate("UserManagementView");            
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