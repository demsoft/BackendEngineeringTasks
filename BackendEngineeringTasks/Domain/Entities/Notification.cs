using BackendEngineeringTasks.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace BackendEngineeringTasks.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsRead { get; set; }
    }
}
