using BackendEngineeringTasks.Domain.Entities;

namespace BackendEngineeringTasks.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int userId);
        Task<IEnumerable<User>> GetAllAsync();
        //Task<IEnumerable<User>> GetUsersByTaskAsync(int taskId);
        Task<User> CreateUserAsync(User user);
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(int userId);
    }
}
