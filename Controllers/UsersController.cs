using CommentsApp.Entities;
using CommentsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommentsApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _usersRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUsersRepository usersRepository, ILogger<UsersController> logger)
        {
            _usersRepository = usersRepository;
            _logger = logger;
        }

        [HttpGet("GetUserAndComments")]
        public async Task<IActionResult> GetUserAndComments(int userId)
        {
            try
            {
               var result = await _usersRepository.GetUserComments(userId);
               return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error getting values");
            }
        }

        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser(User user, int userAdminId)
        {
            try
            {
                await _usersRepository.AddUser(user, userAdminId);
                return Ok(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int userId, int userAdminId)
        {
            try
            {
                await _usersRepository.DeleteUser(userId, userAdminId);
                return Ok(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
