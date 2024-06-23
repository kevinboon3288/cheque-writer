
namespace ChequeWriter.Modules.NotificationModule.Core;

public class NotificationManager
{
    private readonly IEventAggregator _eventAggregator;

    public NotificationManager(IEventAggregator eventAggregator)
    {
        _eventAggregator = eventAggregator;
    }

    public void ShowNotification(string message)
    {
        _eventAggregator.GetEvent<NotificationEvent>().Publish(message);
    }
}
