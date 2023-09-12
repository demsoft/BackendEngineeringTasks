using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Enums;
using BackendEngineeringTasks.Domain.Repositories;
using BackendEngineeringTasks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendEngineeringTasks.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _dbContext; // Assuming Entity Framework Core for database access

        public TaskRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Tasks> GetByIdAsync(int taskId)
        {
            return await _dbContext.Tasks.FindAsync(taskId);
        }

        public async Task<IEnumerable<Tasks>> GetAllAsync()
        {
            return await _dbContext.Tasks.ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetByStatusAsync(Status status)
        {
            return await _dbContext.Tasks.Where(t => t.Status == status).ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetByPriorityAsync(Priority priority)
        {
            return await _dbContext.Tasks.Where(t => t.Priority == priority).ToListAsync();
        }

        public async Task<IEnumerable<Tasks>> GetDueTasksForCurrentWeekAsync()
        {
            var currentDate = DateTime.UtcNow;
            var startOfWeek = currentDate.Date.AddDays(-(int)currentDate.DayOfWeek);
            var endOfWeek = startOfWeek.AddDays(7);

            return await _dbContext.Tasks
                .Where(t => t.DueDate >= startOfWeek && t.DueDate < endOfWeek)
                .ToListAsync();
        }

        public async Task CreateAsync(Tasks task)
        {
            _dbContext.Tasks.Add(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Tasks task)
        {
            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int taskId)
        {
            var taskToDelete = await GetByIdAsync(taskId);
            if (taskToDelete != null)
            {
                _dbContext.Tasks.Remove(taskToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
