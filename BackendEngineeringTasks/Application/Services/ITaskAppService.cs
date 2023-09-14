using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Domain.Enums;

namespace BackendEngineeringTasks.Application.Services
{
    public interface ITaskAppService
    {
        Task AssignTaskToProjectAsync(int taskId, int projectId);
        Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(Status status);
        Task<IEnumerable<TaskDto>> GetTasksByPriorityAsync(Priority priority);
        Task<IEnumerable<TaskDto>> GetDueTasksForCurrentWeekAsync();
        Task SendNotificationsForDueTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(int taskId);
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> CreateTaskAsync(TaskDto taskDto);
        Task UpdateTaskAsync(int taskId, TaskDto taskDto);
        Task DeleteTaskAsync(int taskId);
        Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(int projectId);
        Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId);
    }
}
