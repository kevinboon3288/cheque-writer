using System.Windows.Threading;

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
        Notification notification = new Notification() { Message = message };

        Notifications.Add(notification);

        DispatcherTimer timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
        timer.Tick += (s, e) =>
        {
            Notifications.Remove(notification);
            timer.Stop();
        };
        timer.Start();
    }
}
