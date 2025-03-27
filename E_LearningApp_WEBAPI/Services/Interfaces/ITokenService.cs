using E_LearningApp_WEBAPI.Models;
namespace E_LearningApp_WEBAPI.Services.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
