using BackendEngineeringTasks.Domain.Entities;

namespace BackendEngineeringTasks.Domain.Repositories
{
    public interface IProjectRepository
    {
        Task<Project> GetByIdAsync(int projectId);
        Task<IEnumerable<Project>> GetAllAsync();
        Task CreateAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(int projectId);
    }
}
