using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Interface
{
    public interface ILikeRepository
    {
        Task<Like> AddLike(Post post, Like like);
        Task RemoveLike(Post post, Like like);
        Task<Like> GetLikeByPostIdAndUserId(Guid postId, Guid userId);
    }
}
