using AutoMapper;
using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Domain.Entities;

namespace BackendEngineeringTasks.Application.Mapping
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
        }
    }
}
