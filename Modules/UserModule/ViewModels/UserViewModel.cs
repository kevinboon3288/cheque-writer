namespace ChequeWriter.Modules.UserModule.ViewModels
{
    public class UserViewModel: BindableBase, INavigationAware
    {
        private readonly IRegionManager _regionManager;

        public UserViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            // Entry Point when navigate to UserView
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
