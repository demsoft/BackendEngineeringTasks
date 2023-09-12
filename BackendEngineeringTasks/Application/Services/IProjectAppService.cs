using BackendEngineeringTasks.Application.DTOs;

namespace BackendEngineeringTasks.Application.Services
{
    public interface IProjectAppService
    {
        Task<IEnumerable<ProjectDto>> GetAllProjectsAsync();
        Task<ProjectDto> GetProjectByIdAsync(int projectId);
        Task<ProjectDto> CreateProjectAsync(ProjectDto projectDto);
        Task UpdateProjectAsync(int projectId, ProjectDto projectDto);
        Task DeleteProjectAsync(int projectId);
    }
}
