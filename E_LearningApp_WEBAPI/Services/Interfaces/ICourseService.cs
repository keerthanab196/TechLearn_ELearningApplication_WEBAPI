namespace E_LearningApp_WEBAPI.Services.Interfaces
{
    public interface ICourseService
    {
        Task GetAllApprovedCourses(int pageNumber, int pageSize, string? search, string sortBy, bool isDescending);
    }
}
