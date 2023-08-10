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
    public class PostController : ControllerBase
    {
        private IPostService postService;
        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Post>> AddPost(CreatePostDto postDto)
        {
            try
            {
                return Ok(await postService.AddPost(postDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet, Authorize]
        public async Task<ActionResult<Post>> GetAllPosts(Guid userId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await postService.GetAllPosts(userId));
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

        [HttpGet("GetPostByUserId"), Authorize]
        public async Task<ActionResult<Post>> GetPostById(Guid postId)
        {
            try
            {
                return Ok(await postService.GetPostById(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetUserPosts"), Authorize]
        public async Task<ActionResult<Post>> GetPostsByUserId(Guid userId)
        {
            try
            {
                return Ok(await postService.GetPostsByUserId(userId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Authorize]
        public async Task<ActionResult<Post>> DeletePost(DeletePostDto deletePostDto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await postService.DeletePost(authUserId, deletePostDto));
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
