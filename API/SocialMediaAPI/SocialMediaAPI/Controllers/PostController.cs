using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.BLL.Services;
using SocialMediaAPI.DAL.Models;

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
    }
}
