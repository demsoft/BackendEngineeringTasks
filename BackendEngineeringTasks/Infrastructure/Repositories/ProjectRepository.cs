using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Repositories;
using BackendEngineeringTasks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendEngineeringTasks.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly DataContext _dbContext;

        public ProjectRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Project> GetByIdAsync(int projectId)
        {
            return await _dbContext.Projects.FindAsync(projectId);
        }

        public async Task<IEnumerable<Project>> GetAllAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task CreateAsync(Project project)
        {
            _dbContext.Projects.Add(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Project project)
        {
            _dbContext.Projects.Update(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int projectId)
        {
            var project = await _dbContext.Projects.FindAsync(projectId);
            if (project != null)
            {
                _dbContext.Projects.Remove(project);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
