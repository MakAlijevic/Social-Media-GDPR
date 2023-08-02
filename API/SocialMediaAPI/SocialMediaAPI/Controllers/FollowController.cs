using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Models;

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
                return Ok(await followService.AddFollow(follow));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
