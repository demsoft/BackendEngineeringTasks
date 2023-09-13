using BackendEngineeringTasks.Domain.Enums;

namespace BackendEngineeringTasks.Application.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public int TaskId { get; set; }
        public int UserId { get; set; }
    }
}
