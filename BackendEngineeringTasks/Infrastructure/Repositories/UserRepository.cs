using BackendEngineeringTasks.Domain.Entities;
using BackendEngineeringTasks.Domain.Repositories;
using BackendEngineeringTasks.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BackendEngineeringTasks.Infrastructure.Repositories
{
    public class UserRepository  : IUserRepository
    {
        private readonly DataContext _dbContext;

        public UserRepository(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetByIdAsync(int userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _dbContext.Users.ToListAsync();
        }

        //public async Task<IEnumerable<User>> GetUsersByTaskAsync(int taskId)
        //{
        //    return await _dbContext.Users
        //        .Where(u => u.Tasks.Any(t => t.Id == taskId))
        //        .ToListAsync();
        //}

        public async Task<User> CreateUserAsync(User user)
        {
            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Entry(user).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
