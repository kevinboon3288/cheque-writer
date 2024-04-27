namespace ChequeWriter.Modules.UserModule.ViewModels
{
    public class UserManagementViewModel: BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;
        private readonly IUserManager _userManager;
        private readonly IEventAggregator _eventAggregator;
        private List<User> _users = new();

        public List<User> Users 
        {
            get { return _users; } 
            set {  SetProperty(ref _users, value); }
        }

        public DelegateCommand<User> DeleteUserCommand { get; private set; }
        public DelegateCommand NavigateToUserAdderCommand { get; private set; }
        public DelegateCommand NavigateBack { get; private set; }

        public UserManagementViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IUserManager userManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _userManager = userManager;

            NavigateBack = new DelegateCommand(OnReturn);
            NavigateToUserAdderCommand = new DelegateCommand(OnNavigateToUserAdder);
            DeleteUserCommand = new DelegateCommand<User>(OnDeleteUser);
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _eventAggregator.GetEvent<UIControlEvent>().Publish("User Management");

            OnRefresh();
        }

        private void OnRefresh() 
        {
            Users = _userManager.GetAllUsers();
        }

        private void OnReturn()
        {
            IRegion region = _regionManager.Regions["UserContentRegion"];
            region.RequestNavigate("MainView");
        }

        private void OnNavigateToUserAdder()
        {
            //TODO: Navigate to UserAdderView
            //IRegion region = _regionManager.Regions["UserContentRegion"];
            //region.RequestNavigate("MainView");
        }

        private void OnDeleteUser(User selectedUser) 
        {
            if (selectedUser != null) 
            {
                //TODO: Call Delete API
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
}
