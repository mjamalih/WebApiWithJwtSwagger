using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiWithJwtSwagger.Models;

namespace WebApiWithJwtSwagger.Helper
{
    public class AuthHelpers
    {
       
        public static string GenerateJWTToken(UserInfo user,IConfiguration _configuration)
        {
            

            var claims = new List<Claim> {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
    };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(
                       Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"])
                        ),
                    SecurityAlgorithms.HmacSha256Signature)
                );

           // "ApplicationSettings:JWT_Secret

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
