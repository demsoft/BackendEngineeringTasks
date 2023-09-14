using AutoMapper;
using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Enums;
using BackendEngineeringTasks.Domain.Exceptions;
using BackendEngineeringTasks.Domain.Repositories;
using BackendEngineeringTasks.Infrastructure.Repositories;
using System.Runtime.InteropServices;

namespace BackendEngineeringTasks.Application.Services
{
    public class TaskAppService  : ITaskAppService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly INotificationAppService _notificationService;
        private readonly IProjectAppService _projectservice;
        private readonly IMapper _mapper; // AutoMapper for mapping DTOs
        public TaskAppService(ITaskRepository taskRepository, INotificationAppService notificationService, IProjectAppService projectservice, IMapper mapper)
        {
           _taskRepository = taskRepository;
            _notificationService = notificationService;
            _projectservice = projectservice;
            _mapper = mapper;
        }

        public async Task AssignTaskToProjectAsync(int taskId, int projectId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            var project = await _projectservice.GetProjectByIdAsync(projectId);

            if (task == null)
            {
                throw new NotFoundException($"Task with ID {taskId} not found.");
            }

            if (project == null)
            {
                throw new NotFoundException($"Project with ID {projectId} not found.");
            }

            task.ProjectId = projectId;
            await _taskRepository.UpdateAsync(task);
        }

        public async Task<TaskDto> CreateTaskAsync(TaskDto taskDto)
        {
            try
            {
                var task = _mapper.Map<Tasks>(taskDto);
                //var task = new Tasks
                //{
                //    Id = taskDto.Id,
                //    Title = taskDto.Title,
                //    Description = taskDto.Description,
                //    Status = (Status)taskDto.Status,
                //    Priority = (Priority)taskDto.Priority,
                //    DueDate = taskDto.DueDate,
                //};
                await _taskRepository.CreateAsync(task);
                return _mapper.Map<TaskDto>(task);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task DeleteTaskAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            if (task == null)
            {
                throw new NotFoundException("Tasks not found");
            }

            await _taskRepository.DeleteAsync(taskId);
        }

        public async Task<IEnumerable<TaskDto>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<IEnumerable<TaskDto>> GetDueTasksForCurrentWeekAsync()
        {
            var tasks = await _taskRepository.GetDueTasksForCurrentWeekAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<TaskDto> GetTaskByIdAsync(int taskId)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);
            return _mapper.Map<TaskDto>(task);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByProjectIdAsync(int projectId)
        {
            var task = await _taskRepository.GetTasksByProjectIdAsync(projectId);
            return _mapper.Map< IEnumerable<TaskDto>>(task);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByUserIdAsync(int userId)
        {
            var task = await _taskRepository.GetTasksByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TaskDto>>(task);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByPriorityAsync(Priority priority)
        {
            var tasks = await _taskRepository.GetByPriorityAsync(priority);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(Status status)
        {
            var tasks = await _taskRepository.GetByStatusAsync(status);
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }

        public async Task SendNotificationsForDueTasksAsync()
        {
            var notificationThreshold = DateTime.UtcNow.AddHours(48);

            // Retrieve tasks with due dates within the next 48 hours
            var dueTasks = await _taskRepository.GetTasksDueWithin48HoursAsync(notificationThreshold);

            foreach (var task in dueTasks)
            {
                // Check if a notification should be sent for this task
                if (ShouldSendNotification(task))
                {
                    // Create a notification
                    var notification = new NotificationDto
                    {
                        Type = NotificationType.DueDateReminder,
                        Message = $"Reminder: Task '{task.Title}' is due in 48 hours.",
                        Timestamp = DateTime.UtcNow,
                        TaskId = task.Id, // Optionally, associate the notification with the task
                        UserId = task.UserId // Optionally, associate the notification with the user
                    };

                    // Send the notification (e.g., store it in a notification queue, send it via email, etc.)
                    await _notificationService.CreateNotificationAsync(notification);
                }
            }
        }

        public async Task UpdateTaskAsync(int taskId, TaskDto taskDto)
        {
            var existingTask = await _taskRepository.GetByIdAsync(taskId);
            if (existingTask == null)
            {
                throw new NotFoundException("Task not found");
            }

            _mapper.Map(taskDto, existingTask);
            await _taskRepository.UpdateAsync(existingTask);
        }

        private bool ShouldSendNotification(Tasks task)
        {
            // Implement your logic to determine if a notification should be sent for the task
            // For example, you can check if the task's due date is within 48 hours and it's not completed
            var notificationThreshold = DateTime.UtcNow.AddHours(48);
            return task.DueDate <= notificationThreshold && task.Status != Status.Completed;
        }
    }
}
