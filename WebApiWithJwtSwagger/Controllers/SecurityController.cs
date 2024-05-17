using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApiWithJwtSwagger.Helper;
using WebApiWithJwtSwagger.Models;

namespace WebApiWithJwtSwagger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        DatabaseContext _context;
        IConfiguration _configuaration;
        public SecurityController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuaration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> getToken([FromBody] UserInfo user)
        {

            //UserInfo user = new UserInfo()
            //{
            //    UserName = "ali",
            //    Password = "123"
            //};
            if (_context.Users.Any(x => x.UserName == user.UserName && x.Password == user.Password))
            {
                var token = AuthHelpers.GenerateJWTToken(user, _configuaration);
                return Ok(new { token = token });
            }
            else
                return Ok("Failed  Try Again");
        }
    }
}
