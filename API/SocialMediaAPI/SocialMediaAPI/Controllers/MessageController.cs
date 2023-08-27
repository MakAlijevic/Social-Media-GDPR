using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.BLL.Services;
using SocialMediaAPI.DAL.Models;
using SocialMediaAPI.HubConfig;
using System.Security.Claims;

namespace SocialMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private IMessageService messageService;
        private IHubContext<MessageHub> messageHub;
        public MessageController(IMessageService messageService, IHubContext<MessageHub> messageHub)
        {
            this.messageService = messageService;
            this.messageHub = messageHub;
        }

        [HttpPost, Authorize]
        public async Task<ActionResult<Message>> AddMessage(AddMessageDto message)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }

                await messageHub.Clients.All.SendAsync("MessageAdded");
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

        [HttpPost("StartAChat"), Authorize]
        public async Task<ActionResult<Message>> StartAChat(NewChatDto newChatDto)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }

                await messageHub.Clients.All.SendAsync("MessageAdded");
                return Ok(await messageService.StartAChat(authUserId, newChatDto));
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

        [HttpGet("GetAllMessagesBetweenFriends"), Authorize]
        public async Task<ActionResult<Message>> GetAllMessagesBetweenFriends(Guid FollowerId, Guid FollowingId)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }

                return Ok(await messageService.GetAllMessagesBetweenFriends(FollowerId, FollowingId));
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

        [HttpGet("CheckIfChatExists"), Authorize]
        public async Task<ActionResult<bool>> CheckIfChatExists(Guid FollowerId, Guid FollowingId)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }

                return Ok(await messageService.CheckIfChatExists(FollowerId, FollowingId));
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

        [HttpGet("getAllFriendsForMessages"), Authorize]
        public async Task<ActionResult<List<ReturnUserDto>>> GetAllFriendsForMessages(Guid userId)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }

                return Ok(await messageService.GetAllFriendsForMessages(authUserId, userId));
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
