using AutoMapper;
using BackendEngineeringTasks.Application.DTOs;

namespace BackendEngineeringTasks.Application.Mapping
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<Task, TaskDto>();
            CreateMap<TaskDto, Task>();
        }
    }
}
