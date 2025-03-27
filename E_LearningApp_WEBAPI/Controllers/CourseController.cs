using E_LearningApp_WEBAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_LearningApp_WEBAPI.Controllers
{
    [Route("Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        ELearningProjectDbContext _context;
        public CourseController(ELearningProjectDbContext con)
        {
                _context = con;
        }
        // GET: api/<CourseController>
        
        [HttpGet]
        [Route("GetAllApprovedCourses")]
        public IActionResult GetAllApprovedCourses(int pageNumber=1,int pageSize=10,string? search=null,string sortBy="CreatedAt",bool isDescending=true)
        {
            try
            {
                var query = _context.Courses.Where(c=>c.Status=="Approved").AsQueryable();

                if(!string.IsNullOrEmpty(search))
                {
                    query = query.Where(c => c.Title.Contains(search));
                }

                query = isDescending ? query.OrderByDescending(c => EF.Property<Object>(c, sortBy)) 
                    : query.OrderBy(c => EF.Property<object>(c, sortBy));
                int totalRecords=query.Count();

                var courses = query.Skip((pageNumber - 1) * pageSize).Take(pageSize)
                    .Select(c => new { c.CourseId, c.Title, c.Description, c.CreatedBy, c.CreatedAt, c.Price }).ToList();

                return Ok(new
                {
                    TotalRecords=totalRecords,Page=pageNumber,PageSize=pageSize,Data=courses
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
            
            //return new string[] { "value1", "value2" };
        }

        
        [HttpGet]
        [Route("getCourseByInstructor")]
        public IActionResult GetCourseByInstructor()
        {
            try
            {
                var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                var courses = _context.Courses.Where(c => c.CreatedBy == int.Parse(userId.Value)).Select(c => new { c.CourseId,
                    c.Title, c.Description, c.CreatedAt, c.Price, c.LastUpdated, c.Status, c.CreatedBy }).ToList();

                if(courses==null)
                {
                    return BadRequest("No courses to show");
                }

                return Ok(courses);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        [Route("GetPendingCourses")]
        public IActionResult GetPendingCourses()
        {
            try
            {
                var courses = _context.Courses.Where(c => c.Status == "Pending")
                    .Select(c => new { c.CourseId, c.CreatedBy, c.CreatedAt, c.LastUpdated, c.Description, c.Status })
                    .ToList();

                if(courses==null)
                {
                    return NotFound("There are no pending courses");
                }
                return Ok(courses);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("ApproveCourse/{id}")]
        public IActionResult ApproveCourse(int id)
        {
            try
            {
              var course=  _context.Courses.Find(id);
                if (course == null)
                {
                    return NotFound();
                }
                course.Status = "Approved";
                _context.Courses.Update(course);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("RejectCourse/{id}")]
        public IActionResult RejectCourse(int id)
        {
            try
            {
                var course = _context.Courses.Find(id);
                if (course == null)
                {
                    return NotFound();
                }
                course.Status = "Rejected";
                _context.Courses.Update(course);
                _context.SaveChanges();
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        // POST api/<CourseController>
        [Authorize(Roles ="1,2")]
        [HttpPost]
        [Route("AddCourse")]
        public IActionResult PostCourse([FromBody] CourseDTO courseDto)
        {
            try
            {
                Course course = new Course();
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null)
                {
                    return Unauthorized("User Not found");
                }
                course.Title = courseDto.Title;
                course.Description = courseDto.Description;
                course.Price = courseDto.Price;
                course.CreatedBy = int.Parse(userIdClaim.Value);
                course.Status = "Pending";
                course.CreatedAt = DateTime.Now;
                course.LastUpdated = DateTime.Now;
                _context.Courses.Add(course);
                _context.SaveChanges();
                return Ok();
            }
            catch(Exception e)
            { return BadRequest(e); }
            
        }

        // PUT api/<CourseController>/5
        [Authorize(Roles ="1,2")]
        [HttpPut]
        [Route("UpdateCourse/{id}")]
        public IActionResult EditCourse(int id, [FromBody] CourseDTO courseDTO)
        {
            Course course = new Course();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdClaim = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User Not found");
            }
            course=_context.Courses.FirstOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                return BadRequest("Course not found");
            }
            course.Title = courseDTO.Title;
            course.Description = courseDTO.Description;
            course.Price = courseDTO.Price;
            course.LastUpdated = DateTime.Now;
            _context.Courses.Update(course);
            _context.SaveChanges();
            return Ok();
        }

        // DELETE api/<CourseController>/5
        [HttpDelete]
        [Route("DeleteCourse{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                var course = await _context.Courses.FindAsync(id);
                if (course == null)
                {
                    return NotFound("course doesn't exist");
                }
                _context.Courses.Remove(course);
                await _context.SaveChangesAsync();
                return Ok("Deleted the course");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPost]
        [Route("UploadCourseContent")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadCourseContent(int courseId,string title,string contentType,IFormFile file)
        {
            
            try
            {
                if(file==null||file.Length==0)
                {
                    return BadRequest("No file uploaded");
                }

                var allowedTypes=new List<string> {"application/pdf","video/mp4","video/avi","video/mkv" };
                if(!allowedTypes.Contains(file.ContentType))
                {
                    return BadRequest("Invalid file type, Only pdf and videos can be uploaded");
                }
                var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ContentsUpload");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                      
                }
                var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var filePath= Path.Combine(folder, fileName);

                using (var stream =new FileStream(filePath,FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                var courseCon = new CourseContent()
                {
                    CourseId = courseId,
                    Title = title,
                    ContentType = contentType,
                    CreatedAt = DateTime.Now,
                    Filepath = $"/ContentsUpload/{fileName}"
                };

                
                
                _context.CourseContents.Add(courseCon);
                await _context.SaveChangesAsync();
                return Ok(new { message="Content uploaded successfully"});
            }
            catch(Exception e)
            {
                return BadRequest("Error uploading file "+e.Message);
            }


        }
    }
}
