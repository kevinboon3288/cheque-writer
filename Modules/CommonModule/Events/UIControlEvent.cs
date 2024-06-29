namespace ChequeWriter.Modules.CommonModule.Events;

public class HeaderTitleUIControlEvent : PubSubEvent<string> { }

public class CurrentUserEvent : PubSubEvent<Dictionary<string, dynamic>> { }

public class PreviewChequeEvent : PubSubEvent<Dictionary<string, dynamic>> { }

public class NotificationEvent : PubSubEvent<string> { }