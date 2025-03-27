using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using E_LearningApp_WEBAPI.Repositories.Interfaces;
using E_LearningApp_WEBAPI.Services.Implementations;
using E_LearningApp_WEBAPI.Models;

namespace ELearning_WebAPI.Tests.ServicesTests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> mockuserRepository;
        private readonly UserService _userService;
        public UserServiceTests()
        {
            mockuserRepository=new Mock<IUserRepository>();
            _userService=new UserService(mockuserRepository.Object);
        }

        [Fact]
        public async Task RegisterStudentAsyncReturnsNewUser()
        {
            var userDTO = new UserDTO
            {
                Email = "kbc@gb.com",
                FullName = "Keerthana B",
                PasswordHash = "Keer2333"
            };

            mockuserRepository.Setup(s => s.ValidateEmail(userDTO.Email)).ReturnsAsync(false);
            var result=await _userService.RegisterStudentAsync(userDTO);

            Assert.NotNull(result);
            Assert.Equal(userDTO.FullName, result.FullName);
            Assert.Equal(userDTO.Email, result.Email);

            mockuserRepository.Verify(repo => repo.AdduserAsync(It.IsAny<User>()), Times.Once);
            mockuserRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task RegisterStudentAsyncThrowsExceptionInValidEmail()
        {
            var userDTO = new UserDTO
            {
                Email = "ahhc",
                FullName = "Keerthana",
                PasswordHash = "Keee111"
            };

           
            var ex = await Assert.ThrowsAsync<Exception>(() => _userService.RegisterStudentAsync(userDTO));
            

            Assert.Equal("Invalid email format", ex.Message);


        }


        [Fact]
        public async Task RegisterStudentAsyncThrowsExceptionUserExists()
        {
            var userDTO = new UserDTO
            {
                Email = "keerthana@nknjk.com",
                FullName = "gjhgjhj",
                PasswordHash = "keerthan"
            };

            mockuserRepository.Setup(m => m.ValidateEmail(userDTO.Email)).ReturnsAsync(true);


            var ex = await Assert.ThrowsAsync<Exception>(() => _userService.RegisterStudentAsync(userDTO));


            Assert.Equal("An account already exist with this email", ex.Message);


        }
    }
}
