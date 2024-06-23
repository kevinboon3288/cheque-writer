namespace ChequeWriter.Modules.NotificationModule;

public class NotificationModule: IModule
{
    private readonly IRegionManager _regionManager;

    public NotificationModule(IRegionManager regionManager)
    {
        _regionManager = regionManager;
    }

    public void OnInitialized(IContainerProvider containerProvider)
    {
        _regionManager.RegisterViewWithRegion("NotificationContentRegion", typeof(NotificationView));
    }

    public void RegisterTypes(IContainerRegistry containerRegistry)
    {
        ViewModelLocationProvider.Register<NotificationView, NotificationViewModel>();
    }
}
