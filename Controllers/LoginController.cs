using CommentsApp.context;
using CommentsApp.Models;
using CommentsApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Web.Helpers;

namespace CommentsApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _repository;
        private readonly IUsersRepository _usersRepository;

        public LoginController(IConfiguration configuration, ApplicationDbContext repository, IUsersRepository usersRepository)
        {
            _configuration = configuration;
            _repository = repository;
            _usersRepository = usersRepository;

        }

        [HttpPost]
        public IActionResult Post(Userlogin userInput)
        {
            //Podria hacerlo mejor:
            //Your logic for login process
            //If login username and password are correct then proceed to generate token
            string jwtValue = string.Empty;
            try
            {               
                var currentUser = _repository.User.Where(x => x.UserName == userInput.UserName).FirstAsync().Result;
                if (currentUser != null)
                {
                var userCheck = _usersRepository.CheckUser(currentUser?.Password, userInput.Password);
                    if (userCheck)
                    {
                        var signingCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(
                                            Encoding.UTF8.GetBytes(
                                                _configuration["JWT:SigningKey"])),
                                        SecurityAlgorithms.HmacSha256);

                        var jwtObject = new JwtSecurityToken(
                                        issuer: _configuration["JWT:Issuer"],
                                            audience: _configuration["JWT:Audience"],
                                            expires: DateTime.Now.AddMinutes(10),
                                            signingCredentials: signingCredentials);

                        //Issue with the UTCdateTime so the validation is not taking in consideration the expires prop
                        jwtValue = new JwtSecurityTokenHandler().WriteToken(jwtObject);
                    }
                    else
                    {
                        throw new Exception("User dont match");
                    }
                }

                    return Ok(new { Token = jwtValue, UserName = currentUser.UserName, id = currentUser.Id, isAdmin = currentUser.isAdminUser });
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError);
            }


        }
    }
}