using AutoMapper;
using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Domain.Entities;

namespace BackendEngineeringTasks.Application.Mapping
{
    public class ProjectMappingProfile : Profile
    {
        public ProjectMappingProfile()
        {
            CreateMap<Project, ProjectDto>();
            CreateMap<ProjectDto, Project>();
        }
    }
}
