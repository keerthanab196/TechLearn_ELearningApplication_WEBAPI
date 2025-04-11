using E_LearningApp_WEBAPI.Models;
using E_LearningApp_WEBAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Globalization;

namespace E_LearningApp_WEBAPI.Repositories.Implementations
{
    public class CourseRepository:ICourseRepository
    {
        private readonly ELearningProjectDbContext _context;    
        public CourseRepository(ELearningProjectDbContext context) {
            _context = context;
        }

        public async Task<(List<Course>,int)> GetApprovedCourses(int pageNumber, int pageSize, string? search, string sortBy, bool isDescending)
        {
            var query = _context.Courses.Where(c => c.Status == "Approved").AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Title.Contains(search));
            }

            query = isDescending ? query.OrderByDescending(c => EF.Property<Object>(c, sortBy))
                : query.OrderBy(c => EF.Property<object>(c, sortBy));
            int totalRecords = await query.CountAsync();
            var pagedCourses = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();

            return (pagedCourses, totalRecords);


        }
    }
}
