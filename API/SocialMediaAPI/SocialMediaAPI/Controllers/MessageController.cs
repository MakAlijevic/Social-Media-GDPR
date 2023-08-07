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
    public class MessageController : ControllerBase
    {
        private IMessageService messageService;
        public MessageController(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Message>> AddMessage(AddMessageDto message)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.SerialNumber);
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }

                return Ok(await messageService.AddMessage(authUserId, message));
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
