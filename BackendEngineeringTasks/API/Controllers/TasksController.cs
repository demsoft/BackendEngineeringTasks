using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Application.Services;
using BackendEngineeringTasks.Domain.Enums;
using BackendEngineeringTasks.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace BackendEngineeringTasks.API.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskAppService _tasks;
        public TasksController(ITaskAppService tasks)
        {
            _tasks = tasks; 
        }

        [HttpGet("get/by/priority/{priority}")]
        public async Task<IActionResult> GetTaskByIdAsync(Priority priority)
        {
            var task = await _tasks.GetTasksByPriorityAsync(priority);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpGet("get/by/status/{status}")]
        public async Task<IActionResult> GetTasksByStatusAsync(Status status)
        {
            var task = await _tasks.GetTasksByStatusAsync(status);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPut("{taskId}/assign/task/to/project/{projectId}")]
        public async Task<IActionResult> AssignTaskToProjectAsync(int taskId, int projectId)
        {
            try
            {
                await _tasks.AssignTaskToProjectAsync(taskId, projectId);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskByIdAsync(int taskId)
        {
            var task = await _tasks.GetTaskByIdAsync(taskId);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotificationAsync(TaskDto taskDto)
        {
            try
            {
                var createdTask = await _tasks.CreateTaskAsync(taskDto);
                return CreatedAtAction(nameof(GetTaskByIdAsync), new { taskId = createdTask.Id }, createdTask);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTaskAsync(int taskId, TaskDto taskDto)
        {
            try
            {
                await _tasks.UpdateTaskAsync(taskId, taskDto);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTaskAsync(int taskId)
        {
            try
            {
                await _tasks.DeleteTaskAsync(taskId);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}
