using BackendEngineeringTasks.Application.DTOs;

namespace BackendEngineeringTasks.Application.Services
{
    public interface INotificationAppService
    {
        Task<NotificationDto> GetNotificationByIdAsync(int notificationId);
        Task<IEnumerable<NotificationDto>> GetByUserIdAsync(int userId);
        Task<IEnumerable<NotificationDto>> GetUnreadByUserIdAsync(int userId);
        Task<NotificationDto> CreateNotificationAsync(NotificationDto notificationDto);
        Task UpdateNotificationAsync(int notificationId, NotificationDto notificationDto);
        Task DeleteNotificationAsync(int notificationId);
        Task MarkNotificationAsReadAsync(int notificationId);
        Task MarkNotificationAsUnreadAsync(int notificationId);
    }
}
