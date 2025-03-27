using E_LearningApp_WEBAPI.Models;
using E_LearningApp_WEBAPI.Repositories.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_LearningApp_WEBAPI.Repositories.Implementations
{
    public class LoginRepository:ILoginRepository
    {
        private readonly ELearningProjectDbContext _context;
        public LoginRepository(ELearningProjectDbContext context)
        {
            _context = context; 
        }

        public async Task<User?> VerifyUser(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public void UpdatePassword(User user)
        {
             _context.Users.Update(user);
           
        }

        public async Task SaveChangesAsync()
        {
           await _context.SaveChangesAsync();
        }
    }
}
