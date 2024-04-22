

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

        public UserManagementViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IUserManager userManager)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _userManager = userManager;
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

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
    }
}
