using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Interface
{
    public interface IPostRepository
    {
        Task<Post> AddPost(Post post);
        Task<List<Post>> GetAllPosts(Guid userId, int pageNumber, int pageSize);
        Task DeletePost(Post post);
        Task<Post> GetPostById(Guid postId);
        Task<List<Post>> GetPostsByUserId(Guid userId);
    }
}
