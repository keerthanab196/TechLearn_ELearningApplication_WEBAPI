using E_LearningApp_WEBAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningApp_WEBAPI.Repositories.Interfaces
{
    public interface ILoginRepository
    {
        Task<User?> VerifyUser(string email);
        void UpdatePassword(User user);
        Task SaveChangesAsync();
    }
}
