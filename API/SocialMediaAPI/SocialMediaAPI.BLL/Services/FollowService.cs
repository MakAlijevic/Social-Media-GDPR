using SocialMediaAPI.BLL.DTO;
using SocialMediaAPI.BLL.Interface;
using SocialMediaAPI.DAL.Interface;
using SocialMediaAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMediaAPI.BLL.Services
{
    public class FollowService : IFollowService
    {
        private IFollowRepository followRepository;
        private IUserRepository userRepository;
        public FollowService(IFollowRepository followRepository, IUserRepository userRepository)
        {
            this.followRepository = followRepository;
            this.userRepository = userRepository;

        }
        public async Task<Follow> AddFollow(AddFollowDto addFollowDto)
        {
            var follower = await userRepository.GetUserById(addFollowDto.FollowerId);
            var following = await userRepository.GetUserById(addFollowDto.FollowingId);
            if(follower == null || following == null)
            {
                throw new Exception("Invalid user id.");
            }
            var existingFollow = await followRepository.CheckExistingFollow(follower.Id, following.Id);
            if(existingFollow != null)
            {
                throw new Exception("User is already followed");
            }

            var follow = new Follow
            {
                FollowerId = follower.Id,
                FollowingId = following.Id,
                CreatedAt = DateTime.Now,
            };

            return await followRepository.AddFollow(follow); 
        }
    }
}
