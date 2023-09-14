using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RestfullAPI_PeduliDiri.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace RestfullAPI_PeduliDiri.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _configuration;
        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private UserRequest AuthUser(UserRequest request)
        {
            UserRequest _request = null;
            if(request.Username == "Fatur" &&  request.Password == "123")
            {
                _request = new UserRequest { Nama = "Fatur" };
            }
            return _request;
        }

        private string GenerateToken(UserRequest request)
        {
            DateTime expires = DateTime.Now.AddMinutes(60);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"],null,
                expires: expires,
                signingCredentials: credentials);
            Console.WriteLine(expires);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(UserRequest request)
        {
            IActionResult response = Unauthorized();
            DateTime expires = DateTime.Now.AddMinutes(60);
            double expiresManip = (expires - DateTime.Now).TotalSeconds;
            string expiresInSeconds = expiresManip.ToString("0");
            var _request = AuthUser(request);
            if (_request != null)
            {
                var token = GenerateToken(_request);
                response = Ok(new { namaUser = _request.Nama, token = token, expires = expires }) ;
            }
            return response;
        }
    }
}
