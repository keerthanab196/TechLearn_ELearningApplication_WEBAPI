using E_LearningApp_WEBAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using E_LearningApp_WEBAPI.Services.Implementations;
using E_LearningApp_WEBAPI.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace E_LearningApp_WEBAPI.Controllers
{
    [Route("UsersLogin")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        ELearningProjectDbContext _contextObj;
        ITokenService _tokenService;
        private readonly ILoginService _loginService;
        

        public LoginController(ELearningProjectDbContext context, ITokenService tokenService, ILoginService loginService)
        {
            _contextObj = context;
            _tokenService = tokenService;
            _loginService = loginService;
        }
        // GET: api/<LoginController>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> UserLogin(LoginDTO loginDTO)
        {

            try
            {
                var (user, token) = await _loginService.UserLogin(loginDTO);
                return Ok(new { user = user, token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        
        

       

        // POST api/<LoginController>
        [Route("changePassword")]
        [HttpPut]
        public IActionResult ChangeUserPassword(ChangePasswordDTO cPDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);

                }
                _loginService.ChangePassword(cPDTO);
                return Ok("Password updated");
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            

        }

        
    }
}
