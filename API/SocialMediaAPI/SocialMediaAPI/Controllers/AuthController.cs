using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.DAL.Models;
using SocialMediaAPI.BLL.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.BLL.Services;
using SocialMediaAPI.HubConfig;
using Microsoft.AspNetCore.SignalR;

namespace SocialMediaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService userService;
        private IConfiguration configuration;
        private IFollowService followService;
        private IHubContext<OnlineFollowingHub> onlineFollowingHub;
        public AuthController(IUserService userService, IConfiguration configuration, IFollowService followService, IHubContext<OnlineFollowingHub> onlineFollowingHub)
        {
            this.userService = userService;
            this.configuration = configuration;
            this.followService = followService;
            this.onlineFollowingHub = onlineFollowingHub;
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> Register (UserRegisterDto request)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new CreateUserDto
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                PolicyId = request.PolicyId
            };
            try
            {
                return Ok(await userService.RegisterUser(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("login"), AllowAnonymous]
        public async Task<ActionResult<string>> Login(UserLoginDto request)
        {
            try
            {
                var user = await userService.GetUserByEmail(request.Email);

                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Wrong password");
                }
                await userService.SetOnline(user.Id);

                string token = CreateToken(user);

                var followsToUpdateOnlineFollowList = await followService.GetAllFollowings(user.Id);

                await onlineFollowingHub.Clients.All.SendAsync("OnlineFollowingUpdate", followsToUpdateOnlineFollowList);

                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout"), Authorize]
        public async Task<ActionResult> Logout(Guid userId)
        {
            try
            {
                var userIdClaim = User.FindFirst("serialNumber");
                if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid authUserId))
                {
                    return BadRequest("Invalid authentication token.");
                }
                await userService.SetOffline(authUserId, userId);
                var followsToUpdateOnlineFollowList = await followService.GetAllFollowings(userId);
                await onlineFollowingHub.Clients.All.SendAsync("OnlineFollowingUpdate", followsToUpdateOnlineFollowList);
                return Ok();
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

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim("serialNumber", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
