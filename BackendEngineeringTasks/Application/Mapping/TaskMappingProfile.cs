using AutoMapper;
using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Domain.Entities;

namespace BackendEngineeringTasks.Application.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Tasks, TaskDto>();
            CreateMap<TaskDto, Tasks>();
                //.ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.ProjectId))
                //.ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));
        }
    }
}
