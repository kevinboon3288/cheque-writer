namespace ChequeWriter.Modules.CommonModule.Events;

public class UIControlEvent: PubSubEvent<string>
{
}

public class CurrentUserEvent : PubSubEvent<Dictionary<string, dynamic>>
{
}
