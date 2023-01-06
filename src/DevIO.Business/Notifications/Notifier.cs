using DevIO.Business.Interfaces.Notifications;

namespace DevIO.Business.Notifications
{
    public class Notifier : INotifier
    {
        private List<Notification> _notifications;

        public Notifier() => _notifications = new List<Notification>();

        public List<Notification> GetNotifications() => _notifications;

        public void Handle(Notification notification) => _notifications.Add(notification);

        public bool HaveNotification() => _notifications.Any();
    }
}
