using E_LearningApp_WEBAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_LearningApp_WEBAPI.Controllers
{
    [Route("Test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        ELearningProjectDbContext _context=new ELearningProjectDbContext();

        [HttpGet]
        [Route("conn_test")]
        public List<UserRole> TestConnectionToDB()
        {
            var users=_context.UserRoles.ToList();
            return (users);
        }
    }
}
