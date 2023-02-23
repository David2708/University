
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using System.IdentityModel.Tokens.Jwt;
using universityApiBackend.Helpers;
using universityApiBackend.Models.DataModels;

namespace universityApiBackend.Controllers
{
    [Route("api/[controller")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController(JwtSettings jwtSettings)
        {
            _jwtSettings= jwtSettings;
        }

        private IEnumerable<User> Logins = new List<User>()
        {
            new User()
            {
                Id= 1,
                Email = "david@gmail.com",
                Name = "Admin",
                Password = "Admin"
            },
            new User()
            {
                Id= 2,
                Email = "pepe@gmail.com",
                Name = "User 1",
                Password = "pepe"
            }
        };

        [HttpPost]
        public IActionResult GetToken(UserLogin userLogin)
        {
            try
            {
                var Token = new USerTokens();
                var valid = Logins.Any(user => user.Name.Equals(userLogin.USerName, StringComparison.OrdinalIgnoreCase));

                if (valid)
                {
                    var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.USerName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new USerTokens()
                    {
                        UserName =  user.Name,
                        EmailId = user.Email,
                        Id = user.Id,
                        GuidID = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                return Ok(Token);

            }catch(Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }

    }
}
