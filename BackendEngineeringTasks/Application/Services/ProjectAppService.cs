using AutoMapper;
using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Exceptions;
using BackendEngineeringTasks.Domain.Repositories;

namespace BackendEngineeringTasks.Application.Services
{
    public class ProjectAppService : IProjectAppService
    {
        private readonly IProjectRepository _projectRepository; // Assuming you have a project repository
        private readonly IMapper _mapper; // AutoMapper for mapping DTOs

        public ProjectAppService(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProjectDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<ProjectDto> GetProjectByIdAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task<ProjectDto> CreateProjectAsync(ProjectDto projectDto)
        {
            var project = _mapper.Map<Project>(projectDto);
            await _projectRepository.CreateAsync(project);
            // You may want to perform additional operations here if needed
            return _mapper.Map<ProjectDto>(project);
        }

        public async Task UpdateProjectAsync(int projectId, ProjectDto projectDto)
        {
            var existingProject = await _projectRepository.GetByIdAsync(projectId);
            if (existingProject == null)
            {
                throw new NotFoundException("Project not found");
            }

            _mapper.Map(projectDto, existingProject);
            await _projectRepository.UpdateAsync(existingProject);
        }

        public async Task DeleteProjectAsync(int projectId)
        {
            var project = await _projectRepository.GetByIdAsync(projectId);
            if (project == null)
            {
                throw new NotFoundException("Project not found");
            }

            await _projectRepository.DeleteAsync(projectId);
        }
    }
}
