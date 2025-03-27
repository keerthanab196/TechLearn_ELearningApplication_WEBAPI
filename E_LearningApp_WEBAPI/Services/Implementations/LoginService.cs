using E_LearningApp_WEBAPI.Models;
using E_LearningApp_WEBAPI.Repositories.Interfaces;
using E_LearningApp_WEBAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;


namespace E_LearningApp_WEBAPI.Services.Implementations
{
    public class LoginService: ILoginService
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ITokenService _tokenService;
        public LoginService(ILoginRepository repository, ITokenService tokenService)
        {
            _loginRepository = repository;
            _tokenService = tokenService;   
        }

        public async Task<(User?,string)> UserLogin(LoginDTO loginDTO)
        {
            if (string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                throw new Exception("Invalid credentials");
            }
            User? user =  await _loginRepository.VerifyUser(loginDTO.Email);


            if (user == null || (!ValidateGivenPassword(loginDTO.Password,user.PasswordHash)))
            {
                throw new Exception("Invalid username or password");
            }

            var token = _tokenService.GenerateToken(user);
            return (user, token);

        }

        public bool ValidateGivenPassword(string? pwd, string? pwdHash)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(pwd);
                byte[] hash = sha256.ComputeHash(bytes);
                var hashPwd = Convert.ToBase64String(hash);
                if (hashPwd == pwdHash)
                {
                    return
                        true;
                }
                return false;
            }
        }

        public async Task ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            if (string.IsNullOrEmpty(changePasswordDTO.Email))
            {
                throw new Exception("Email cannot be null");
            }
            User? user = await _loginRepository.VerifyUser(changePasswordDTO.Email);
            if (user == null || (!ValidateGivenPassword(changePasswordDTO.CurrentPassword, user.PasswordHash)))
            {
                throw new Exception("Invalid username or password");
            }

            if (changePasswordDTO.NewPassword != changePasswordDTO.ConfirmNewPassword)
            {
                throw new Exception("Passwords don't match");
            }
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(changePasswordDTO.NewPassword);
                byte[] hash = sha256.ComputeHash(bytes);
                user.PasswordHash = Convert.ToBase64String(hash);
            }
             _loginRepository.UpdatePassword(user);
             await _loginRepository.SaveChangesAsync();

        }
    }
}
