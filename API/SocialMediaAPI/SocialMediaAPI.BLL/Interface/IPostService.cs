using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Interface
{
    public interface IPostService
    {
        Task<Post> AddPost(CreatePostDto postDto);
        Task<ReturnPostDto> GetPostById(Guid postId);
        Task<string> DeletePost(Guid authUserId, DeletePostDto deletePostDto);
        Task<List<ReturnPostDto>> GetPostsByUserId(Guid userId);
        Task<List<ReturnPostDto>> GetAllPosts(Guid userId);
    }
}
