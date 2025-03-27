using Microsoft.AspNetCore.Mvc;
using E_LearningApp_WEBAPI.Models;
using System.Text.RegularExpressions;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using E_LearningApp_WEBAPI.Services.Interfaces;
using System.Runtime.Intrinsics.X86;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_LearningApp_WEBAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("userRegistration")]

    [ApiController]
    public class UsersController : ControllerBase
    {
        ELearningProjectDbContext _context;
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _context = new ELearningProjectDbContext();
            _userService= userService;
        }


        // POST api/<UsersController>
        [HttpPost]
        [Route("StudentRegistration")]
        public async Task<IActionResult> UserPost(UserDTO user)
        {
            try
            {
               User user1=await _userService.RegisterStudentAsync(user);
               return CreatedAtAction(nameof(UserPost), new { id = user1.UserId }, user1);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }

        [HttpPost]
        [Route("AdminRegistration")]
        public async Task<IActionResult> AdminInstructorPost(UserDTO user)
        {
            try
            {
                User user1 = await _userService.RegisterAdminInstructorAsync(user);

                return CreatedAtAction(nameof(AdminInstructorPost), new { id = user1.UserId }, user1);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
           
        }

       
    }
}
