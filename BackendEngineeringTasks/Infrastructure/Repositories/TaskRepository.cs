using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Enums;
using BackendEngineeringTasks.Domain.Repositories;
using BackendEngineeringTasks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendEngineeringTasks.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DataContext _dbContext;

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

        public async Task<IEnumerable<Tasks>> GetTasksDueWithin48HoursAsync(DateTime thresholdDateTime)
        {
            // Query tasks with due dates within the next 48 hours
            var dueTasks = await _dbContext.Tasks
                .Where(task => task.DueDate <= thresholdDateTime)
                .ToListAsync();

            return dueTasks;
        }

        public async Task<IEnumerable<Tasks>> GetTasksByProjectIdAsync(int projectId)
        {
            var tasksInProject = await _dbContext.Tasks
               .Where(task => task.ProjectId == projectId)
               .ToListAsync();

            return tasksInProject;
        }

        public async Task<IEnumerable<Tasks>> GetTasksByUserIdAsync(int userId)
        {
            var userTasks = await _dbContext.Tasks
             .Where(task => task.UserId == userId)
             .ToListAsync();

            return userTasks;
        }
    }
}
