using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Enums;

namespace BackendEngineeringTasks.Domain.Repositories
{
    public interface ITaskRepository
    {
        Task<Tasks> GetByIdAsync(int taskId);
        Task<IEnumerable<Tasks>> GetAllAsync();
        Task<IEnumerable<Tasks>> GetByStatusAsync(Status status);
        Task<IEnumerable<Tasks>> GetByPriorityAsync(Priority priority);
        Task<IEnumerable<Tasks>> GetDueTasksForCurrentWeekAsync();
        Task CreateAsync(Tasks task);
        Task UpdateAsync(Tasks task);
        Task DeleteAsync(int taskId);
    }
}
