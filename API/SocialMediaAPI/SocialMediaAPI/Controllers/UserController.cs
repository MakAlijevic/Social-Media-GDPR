using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Models;

namespace SocialMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet("GetLoggedInUser"), Authorize]
        public async Task<ActionResult<User>> GetUserById(Guid userId)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await userService.GetLoggedInUser(authUserId));
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

        [HttpGet("SearchAllUsers"), Authorize]
        public async Task<ActionResult<List<ReturnUserDto>>> SearchUserByName(string searchName)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                return Ok(await userService.SearchUsersByName(searchName));
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
