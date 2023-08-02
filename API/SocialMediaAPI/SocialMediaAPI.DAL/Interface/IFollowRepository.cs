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
    }
}
