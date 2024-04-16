namespace ChequeWriter.Modules.UserModule
{
    public class UserModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public UserModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RegisterViewWithRegion("UserContentRegion", typeof(UserView));
            _regionManager.RegisterViewWithRegion("UserLoginContentRegion", typeof(UserLoginView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UserView>();
            containerRegistry.RegisterForNavigation<UserLoginView>();

            ViewModelLocationProvider.Register<UserView, UserViewModel>();
            ViewModelLocationProvider.Register<UserLoginView, UserLoginViewModel>();

        }
    }
}
