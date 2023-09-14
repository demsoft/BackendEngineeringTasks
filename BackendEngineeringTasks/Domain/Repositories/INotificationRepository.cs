using BackendEngineeringTasks.Domain.Entities;

namespace BackendEngineeringTasks.Domain.Repositories
{
    public interface INotificationRepository
    {
        Task<Notification> GetByIdAsync(int notificationId);
        Task<IEnumerable<Notification>> GeAllNotificationAsync();
        Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
        Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId);
        Task AddAsync(Notification notification);
        Task UpdateAsync(Notification notification);
        Task DeleteAsync(int notificationId);
        Task MarkNotificationAsReadAsync(int notificationId);
        Task MarkNotificationAsUnreadAsync(int notificationId);
    }
}
