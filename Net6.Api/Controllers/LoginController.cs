using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Net6.Api.DataTransport;
using Net6.Api.Repository.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Net6.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {     
        private readonly ILogger<LoginController> _logger;
     
        public LoginController(ILogger<LoginController> logger, IActivity activity)
        {
            _logger = logger;        
        }

        [HttpGet("Login/{user}/{password}")]
        public ResponseDto<string> Login(string user, string password)
        {
            var response = new ResponseDto<string>();

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("mTr4KOkSu1lk49hL");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("User", user),
                        new Claim("password", password)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);

                response.Message = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                response.IsCorrect = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}