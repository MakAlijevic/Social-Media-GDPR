using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Models;
using System.Security.Claims;

namespace SocialMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        private IFollowService followService;
        public FollowController(IFollowService followService)
        {
            this.followService = followService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Follow>> AddFollow(AddFollowDto follow)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }

                return Ok(await followService.AddFollow(authUserId, follow));
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
        public async Task<ActionResult<string>> Unfollow(AddFollowDto unfollow)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await followService.Unfollow(authUserId, unfollow));
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

        [HttpGet, Authorize]
        public async Task<ActionResult<List<ReturnFollowDto>>> GetAllFollows(Guid userId)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await followService.GetAllFollows(authUserId, userId));
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
