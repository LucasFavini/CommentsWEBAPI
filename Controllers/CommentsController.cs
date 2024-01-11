using CommentsApp.Entities;
using CommentsApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CommentsApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        private readonly ICommentsRepository _commentsRepository;
        private readonly ILogger<CommentsController> _logger;

        public CommentsController(ICommentsRepository commentsRepository, ILogger<CommentsController> logger)
        {
            _commentsRepository = commentsRepository;
            _logger = logger;
        }


        [HttpGet("GetComents")]        
        public async Task<IActionResult> GetComments()
        {
            _logger.LogInformation($"Getting comments from {nameof(CommentsController)}");
            var result = await _commentsRepository.GetComments();
            return Ok(result);
        }

        [HttpPost("AddComment")]
        public async Task<IActionResult> AddComment(Comment comment)
        {
            try
            {
                _logger.LogInformation($"Start Adding a comment");
                if (ModelState.IsValid && comment.UserId > 0)
                {
                    await _commentsRepository.AddComment(comment);
                    return Ok(StatusCodes.Status204NoContent);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error while was adding a comment");
            }
        }


        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(Comment comment)
        {
            try
            {
                _logger.LogInformation($"Start Deleting a comment");

                await _commentsRepository.UpdateComment(comment);
                return Ok(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error while was deleting a comment");
            }
        }

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int userId, int commentId)
        {
            try
            {
                _logger.LogInformation($"Start Deleting a comment");

                await _commentsRepository.DeleteComment(userId, commentId);
                return Ok(StatusCodes.Status204NoContent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest("Error while was deleting a comment");
            }
        }
    }
}
