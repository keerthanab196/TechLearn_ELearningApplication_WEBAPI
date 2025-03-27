using E_LearningApp_WEBAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningApp_WEBAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> ValidateEmail(string email);
        Task AdduserAsync(User user);
        Task SaveChangesAsync();

    }
}
