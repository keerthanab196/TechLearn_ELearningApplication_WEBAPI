using E_LearningApp_WEBAPI.Services.Interfaces;
using E_LearningApp_WEBAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace E_LearningApp_WEBAPI.Services.Implementations
{
    public class TokenService : ITokenService
    {
        private readonly string _secretKey = "RAN123DOM567KEY__FOR_E_LEARNING_APPLICATION_#123098";
        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.RoleId.ToString()) // Include RoleId in the token
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "ELearning-app",
                audience: "ELearning-app",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
