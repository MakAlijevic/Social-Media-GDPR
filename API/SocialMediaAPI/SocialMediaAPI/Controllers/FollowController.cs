using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
                var userIdClaim = User.FindFirst("serialNumber");
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
                var userIdClaim = User.FindFirst("serialNumber");
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

        [HttpGet("allFollows"), Authorize]
        public async Task<ActionResult<List<ReturnFollowDto>>> GetAllFollows(Guid userId, int pageNumber, int pageSize)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await followService.GetAllFollows(authUserId, userId, pageNumber, pageSize));
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

        [HttpGet("onlineFollows"), Authorize]
        public async Task<ActionResult<List<ReturnFollowDto>>> GetOnlineFollows(Guid userId)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await followService.GetOnlineFollows(authUserId, userId));
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


        [HttpGet("SearchFollowedUsers"), Authorize]
        public async Task<ActionResult<List<ReturnUserDto>>> SearchFollowedUserByName(string searchName)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await followService.SearchFollowedUsersByName(authUserId, searchName));
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
