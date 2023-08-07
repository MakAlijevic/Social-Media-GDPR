using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Interface
{
    public interface IFollowService
    {
        Task<Follow> AddFollow(Guid authUserId, AddFollowDto addFollowDto);
        Task<string> Unfollow(Guid authUserId, AddFollowDto unfollowDto);
        Task<List<ReturnFollowDto>> GetAllFollows(Guid authUserId, Guid userId);
        Task<List<ReturnFollowDto>> GetOnlineFollows(Guid authUserId, Guid userId);
        Task<bool> VerifyExistingFriendship(Guid userId1, Guid userId2);

    }
}
