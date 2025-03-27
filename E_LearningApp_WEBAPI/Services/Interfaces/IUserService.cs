using E_LearningApp_WEBAPI.Models;

namespace E_LearningApp_WEBAPI.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterStudentAsync(UserDTO user);
        Task<User> RegisterAdminInstructorAsync(UserDTO user);
        
    }
}
