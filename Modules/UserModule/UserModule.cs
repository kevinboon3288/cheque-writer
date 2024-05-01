namespace ChequeWriter.Modules.UserModule;

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
        containerRegistry.RegisterForNavigation<UserAdderView>();
        containerRegistry.RegisterForNavigation<UserManagementView>();

        ViewModelLocationProvider.Register<UserView, UserViewModel>();
        ViewModelLocationProvider.Register<UserLoginView, UserLoginViewModel>();
        ViewModelLocationProvider.Register<UserAdderView, UserAdderViewModel>();
        ViewModelLocationProvider.Register<UserManagementView, UserManagementViewModel>();
    }
}
