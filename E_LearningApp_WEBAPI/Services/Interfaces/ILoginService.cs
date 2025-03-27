using E_LearningApp_WEBAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningApp_WEBAPI.Services.Interfaces
{
    public interface ILoginService
    {

        bool ValidateGivenPassword(string pwd, string pwdHash);
        Task<(User?,string)> UserLogin(LoginDTO loginDTO);
        Task ChangePassword(ChangePasswordDTO changePasswordDTO);
    }
}
