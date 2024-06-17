namespace ChequeWriter.Modules.CommonModule.Events;

public class HeaderTitleUIControlEvent: PubSubEvent<string>
{
}

public class CurrentUserEvent : PubSubEvent<Dictionary<string, dynamic>>
{
}
