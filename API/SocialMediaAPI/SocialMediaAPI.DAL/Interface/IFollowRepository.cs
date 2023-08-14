using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.DAL.Interface
{
    public interface IFollowRepository
    {
        Task<Follow> AddFollow(Follow follow);
        Task<Follow> CheckExistingFollow(Guid followerId, Guid followingId);
        Task Unfollow(Follow follow);
        Task<List<Follow>> GetAllFollows(Guid userId, int pageNumber, int pageSize);
        Task<List<Follow>> GetAllFollowsWithoutPagination(Guid userId);
        Task<List<User>> SearchFollowedUsersByName(Guid userId, string searchName);
        Task<List<Follow>> GetAllFollowings(Guid userId);
    }
}
