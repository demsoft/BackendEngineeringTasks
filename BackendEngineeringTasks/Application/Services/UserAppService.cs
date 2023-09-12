using AutoMapper;
using BackendEngineeringTasks.Application.DTOs;
using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Exceptions;
using BackendEngineeringTasks.Domain.Repositories;
using BackendEngineeringTasks.Infrastructure.Repositories;

namespace BackendEngineeringTasks.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _users;
        private readonly IMapper _mapper;

        public UserAppService(IUserRepository users, IMapper mapper)
        {
            _users = users;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _users.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _users.GetByIdAsync(id);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _users.CreateUserAsync(user);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UserDto userDto)
        {
            var existingUser = await _users.GetByIdAsync(id);
            if (existingUser == null)
            {
                throw new NotFoundException("User not found");
            }

            _mapper.Map(userDto, existingUser);
            await _users.UpdateUserAsync(existingUser);
            return _mapper.Map<UserDto>(existingUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _users.GetByIdAsync(id);
            if (user == null)
            {
                throw new NotFoundException("User not found");
            }

            await _users.DeleteUserAsync(id);
            return true;
        }

    }
}
