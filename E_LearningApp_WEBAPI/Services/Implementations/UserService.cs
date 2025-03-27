using E_LearningApp_WEBAPI.Models;
using E_LearningApp_WEBAPI.Repositories.Interfaces;
using E_LearningApp_WEBAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace E_LearningApp_WEBAPI.Services.Implementations
{
    public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repository) {
            _userRepository = repository;
        }

        public async Task<User> RegisterStudentAsync(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.FullName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                throw new Exception("Name,email and password are required fields, please try again");
            }

            if (!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("Invalid email format");
            }

            if(await _userRepository.ValidateEmail(user.Email))
            {
                throw new Exception("An account already exist with this email");
            }
            string passwordHash;
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(user.PasswordHash);
                byte[] hash = sha256.ComputeHash(bytes);
                passwordHash = Convert.ToBase64String(hash);
            }

            var newUser = new User
            {
                FullName = user.FullName,
                Email = user.Email,
                PasswordHash = passwordHash,
                RoleId = 3,
                CreatedAt = DateTime.Now,
                LastLogin = DateTime.Now
            };

            await _userRepository.AdduserAsync(newUser);
            await _userRepository.SaveChangesAsync();

            return newUser;
        }

        public async Task<User> RegisterAdminInstructorAsync(UserDTO user)
        {
            if (string.IsNullOrEmpty(user.FullName) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
            {
                throw new Exception("Name,email and password are required fields, please try again");
            }

            if (!Regex.IsMatch(user.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                throw new Exception("Invalid email format");
            }
            if (await _userRepository.ValidateEmail(user.Email))
            {
                throw new Exception("This email is already taken");
            }
            string passwordHash;
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(user.PasswordHash);
                byte[] hash = sha256.ComputeHash(bytes);
                passwordHash = Convert.ToBase64String(hash);
            }
            var newUser = new User
            {
                FullName = user.FullName,
                Email = user.Email,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.Now,
                LastLogin = DateTime.Now,
                RoleId = user.RoleId

            };
           await _userRepository.AdduserAsync(newUser);
           await  _userRepository.SaveChangesAsync();
           return newUser;
        }
    }
}
