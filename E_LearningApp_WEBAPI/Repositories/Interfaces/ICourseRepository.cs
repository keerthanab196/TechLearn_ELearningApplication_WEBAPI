using E_LearningApp_WEBAPI.Models;

namespace E_LearningApp_WEBAPI.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task<(List<Course>, int)>  GetApprovedCourses(int pageNumber, int pageSize, string? search, string sortBy, bool isDescending);
 

  
    }
}
