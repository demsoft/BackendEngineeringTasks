using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Exceptions;
using BackendEngineeringTasks.Domain.Repositories;
using BackendEngineeringTasks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendEngineeringTasks.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly DataContext _dbContext;

        public NotificationRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Notification> GetByIdAsync(int notificationId)
        {
            return await _dbContext.Notifications.FindAsync(notificationId);
        }

        public async Task<IEnumerable<Notification>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Notification>> GetUnreadByUserIdAsync(int userId)
        {
            return await _dbContext.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();
        }

        public async Task AddAsync(Notification notification)
        {
            _dbContext.Notifications.Add(notification);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Notification notification)
        {
            _dbContext.Notifications.Update(notification);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int notificationId)
        {
            var notification = await _dbContext.Notifications.FindAsync(notificationId);
            if (notification != null)
            {
                _dbContext.Notifications.Remove(notification);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task MarkNotificationAsReadAsync(int notificationId)
        {
            var notification = await GetByIdAsync(notificationId);

            if (notification == null)
            {
                throw new NotFoundException($"Notification with ID {notificationId} not found.");
            }

            // Mark the notification as read (update its status)
            notification.IsRead = true;

            await _dbContext.SaveChangesAsync();
        }

        public async Task MarkNotificationAsUnreadAsync(int notificationId)
        {
            var notification = await GetByIdAsync(notificationId);

            if (notification == null)
            {
                throw new NotFoundException($"Notification with ID {notificationId} not found.");
            }

            // Mark the notification as unread (update its status)
            notification.IsRead = false;

            await _dbContext.SaveChangesAsync();
        }
    }
}
