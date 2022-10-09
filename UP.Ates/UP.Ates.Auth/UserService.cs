using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UP.Ates.Auth.Data;
using UP.Ates.Auth.Models;

namespace UP.Ates.Auth
{
    public class UserService
    {
        private readonly ApplicationDbContext _dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser[]> GetUsers()
        {
            return await _dbContext.Users.ToArrayAsync();
        }

        public async Task AddUser(ApplicationUser user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid userId)
        {
            var userToRemove = await _dbContext.Users.FindAsync(userId.ToString());
            if (userToRemove != null)
                _dbContext.Users.Remove(userToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }
}