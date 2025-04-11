using E_LearningApp_WEBAPI.Repositories.Interfaces;

namespace E_LearningApp_WEBAPI.Services.Implementations
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepository;
        public CourseService(ICourseRepository courseRepository) {
            _courseRepository = courseRepository;

        }

        public async Task GetAllApprovedCourses(int pageNumber, int pageSize, string? search, string sortBy, bool isDescending)
        {
            var (courses,count)= await _courseRepository.GetApprovedCourses(pageNumber, pageSize, search, sortBy, isDescending);




        }

    }
}
