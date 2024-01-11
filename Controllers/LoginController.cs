using CommentsApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CommentsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult Post(Admin admin)
        {
            //Podria hacerlo mejor:
            //Your logic for login process
            //If login username and password are correct then proceed to generate token
            string jwtValue = string.Empty;
            try
            {
                if (admin.Password == "admin" && admin.UserName == "admin")
                {
                    var signingCredentials = new SigningCredentials(
                            new SymmetricSecurityKey(
                                        Encoding.UTF8.GetBytes(
                                            _configuration["JWT:SigningKey"])),
                                    SecurityAlgorithms.HmacSha256);

                    var jwtObject = new JwtSecurityToken(
                                    issuer: _configuration["JWT:Issuer"],
                                        audience: _configuration["JWT:Audience"],
                                        expires: DateTime.Now.AddSeconds(300),
                                        signingCredentials: signingCredentials);
                    jwtValue = new JwtSecurityTokenHandler().WriteToken(jwtObject);
                }
                    return Ok(jwtValue);
            }
            catch (Exception)
            {

                return Unauthorized();
            }


        }
    }
}