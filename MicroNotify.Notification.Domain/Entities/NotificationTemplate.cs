namespace MicroNotify.Notification.Domain.Entities
{
    public class NotificationTemplate
    {
        public Guid Id { get; set; }
        public string EventType { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string MessageTemplate { get; set; } = default!;
    }
}
