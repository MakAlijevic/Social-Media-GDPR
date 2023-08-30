using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.BLL.Services;
using SocialMediaAPI.DAL.Models;
using System.Security.Claims;

namespace SocialMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private ICommentService commentService;
        public CommentController(ICommentService commentService)
        {
            this.commentService = commentService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Comment>> AddComment(AddCommentDto addCommentDto)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await commentService.AddComment(authUserId, addCommentDto));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Like>> DeleteComment(DeleteCommentDto deleteCommentDto)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await commentService.RemoveComment(authUserId, deleteCommentDto));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
