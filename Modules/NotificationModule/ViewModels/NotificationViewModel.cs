namespace ChequeWriter.Modules.NotificationModule.ViewModels;

public class NotificationViewModel: BindableBase
{
    private IEventAggregator _eventAggregator;

    public ObservableCollection<Notification> Notifications { get; } = new();


    public NotificationViewModel(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;

        _eventAggregator.GetEvent<NotificationEvent>().Subscribe(AddNotification);
    }

    private void AddNotification(string message) 
    {
        Notifications.Add(new Notification() { Message = message });
    }
}
