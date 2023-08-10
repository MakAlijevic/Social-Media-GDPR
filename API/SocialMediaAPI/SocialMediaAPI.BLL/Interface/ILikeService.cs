using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Interface
{
    public interface ILikeService
    {
        Task<Like> AddLike(Guid authUserId, AddLikeDto addLikeDto);
        Task<string> RemoveLike(Guid authUserId, DeleteLikeDto deleteLikeDto);
    }
}
