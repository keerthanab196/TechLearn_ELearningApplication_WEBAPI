using E_LearningApp_WEBAPI.Models;
using E_LearningApp_WEBAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace E_LearningApp_WEBAPI.Repositories.Implementations
{
    public class UserRepository:IUserRepository
    {
        private ELearningProjectDbContext _context;
        public UserRepository(ELearningProjectDbContext context) {
            _context = context;
        }

        public async Task<bool> ValidateEmail(string email)
        {
            return await _context.Users.AnyAsync(o => o.Email == email);
        }


        public async Task AdduserAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
